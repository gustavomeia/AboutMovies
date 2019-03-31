using AboutMovies.Helpers;
using AboutMovies.Interfaces;
using AboutMovies.Model;
using AboutMovies.Services.Responses;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AboutMovies.Services {
    public class UpcomingMovieService : IUpcomingMovieService {
        //The API page: https://developers.themoviedb.org/3/movies/get-upcoming 
        private readonly string apiKey = "1f54bd990f1cdfb230adb312546d765d";
        private readonly string apiUrl = "https://api.themoviedb.org/3";
        private GenreRootResponse genreRootResponse;

        //TODO: Need receive the region as a parameter, so that the user can choose the region to filter the results
        public async Task<UpcomingMovie> GetUpcomingMoviesAsync(int page) {
            try {
                string upcomingMoviesPath = "movie/upcoming";

                using (var client = new HttpClient()) {
                    var startPath = Path.Combine(apiUrl, upcomingMoviesPath);
                    var result = await client.GetAsync($"{startPath}?api_key={apiKey}&language=en-US&page={page}");

                    if (!result.IsSuccessStatusCode) {
                        return new UpcomingMovie {
                            Succeeded = false,
                            ErrorMessage = result.ReasonPhrase,
                        };
                    }

                    if (genreRootResponse == null) {
                        genreRootResponse = await FillGenres();

                        if (!genreRootResponse.Succeeded) {
                            return new UpcomingMovie {
                                Succeeded = false,
                                ErrorMessage = string.IsNullOrEmpty(genreRootResponse.ErrorMessage) ? "" : genreRootResponse.ErrorMessage,
                            };
                        }
                    }

                    var resultString = await result.Content.ReadAsStringAsync();
                    var upcomingMoviesRoot = JsonConvert.DeserializeObject<UpcomingMoviesRootResponse>(resultString);
                    return ConverterReponseToModel.UpcomingMovie(upcomingMoviesRoot, genreRootResponse);
                }
            }
            catch (Exception ex) {
                return new UpcomingMovie {
                    Succeeded = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        private async Task<GenreRootResponse> FillGenres() {
            try {
                var genrePath = "genre/movie/list";

                using (var client = new HttpClient()) {
                    var startPath = Path.Combine(apiUrl, genrePath);
                    var result = await client.GetAsync($"{startPath}?api_key={apiKey}&language=en-US");

                    if (result.IsSuccessStatusCode) {
                        var resultString = await result.Content.ReadAsStringAsync();
                        var genreRootResponse = JsonConvert.DeserializeObject<GenreRootResponse>(resultString);
                        genreRootResponse.Succeeded = true;
                        return genreRootResponse;
                    }
                    else {
                        return new GenreRootResponse {
                            ErrorMessage = result.ReasonPhrase,
                            Succeeded = false
                        };
                    }
                }
            }
            catch (Exception ex) {
                return new GenreRootResponse {
                    Succeeded = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
