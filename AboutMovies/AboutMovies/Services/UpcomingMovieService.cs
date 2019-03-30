using AboutMovies.Helpers;
using AboutMovies.Interfaces;
using AboutMovies.Model;
using AboutMovies.Services.Responses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AboutMovies.Services {
    public class UpcomingMovieService : IUpcomingMovieService {
        private readonly string apiKey = "1f54bd990f1cdfb230adb312546d765d";
        private readonly string apiUrl = "https://api.themoviedb.org/3";
        private GenreRootResponse genreRootResponse;

        public async Task<UpcomingMovie> GetUpcomingMoviesAsync(int page) {
            string upcomingMoviesPath = "movie/upcoming";

            using (var client = new HttpClient()) {
                var startPath = Path.Combine(apiUrl, upcomingMoviesPath);
                var result = await client.GetAsync($"{startPath}?api_key={apiKey}&language=en-US&page={page}");

                if (result.IsSuccessStatusCode) {
                    if (genreRootResponse == null) {
                        await FillGenres();
                    }

                    var resultString = await result.Content.ReadAsStringAsync();
                    var upcomingMoviesRoot = JsonConvert.DeserializeObject<UpcomingMoviesRootResponse>(resultString);
                    return ConverterReponseToModel.UpcomingMovie(upcomingMoviesRoot, genreRootResponse);
                }
                else {
                    //TODO: ERROR!!!
                    return new UpcomingMovie();
                }
            }
        }


        private async Task FillGenres() {
            var genrePath = "genre/movie/list";

            using (var client = new HttpClient()) {
                var startPath = Path.Combine(apiUrl, genrePath);
                var result = await client.GetAsync($"{startPath}?api_key={apiKey}&language=en-US");

                if (result.IsSuccessStatusCode) {
                    var resultString = await result.Content.ReadAsStringAsync();
                    genreRootResponse = JsonConvert.DeserializeObject<GenreRootResponse>(resultString);
                }
                else {
                    //TODO: ERROR!!!
                    genreRootResponse = new GenreRootResponse {
                        Genres = new List<GenreResponse>()
                    };
                }
            }
        }
    }
}
