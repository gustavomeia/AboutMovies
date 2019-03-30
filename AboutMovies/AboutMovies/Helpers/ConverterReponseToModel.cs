using AboutMovies.Model;
using AboutMovies.Services.Responses;
using System.Collections.Generic;
using System.Linq;

namespace AboutMovies.Helpers {
    public static class ConverterReponseToModel {
        private static readonly string baseImageUrl = "https://image.tmdb.org/t/p/w500/";

        public static UpcomingMovie UpcomingMovie(UpcomingMoviesRootResponse upcomingMoviesRootResponse, GenreRootResponse genreRootResponse) {
            return new UpcomingMovie {
                Page = upcomingMoviesRootResponse.Page,
                TotalPages = upcomingMoviesRootResponse.TotalPages,
                Movies = upcomingMoviesRootResponse.Results.Select(x => Movie(x, genreRootResponse)).ToList(),
            };
        }

        private static Movie Movie(UpcomingMovieResponse upcomingMovieResponse, GenreRootResponse genreRootResponse) {
            return new Movie {
                Genre = FormatGenres(genreRootResponse, upcomingMovieResponse),
                Name = upcomingMovieResponse.Title,
                PosterUrl = baseImageUrl + upcomingMovieResponse.PosterPath,
                ReleaseDate = upcomingMovieResponse.ReleaseDate
            };
        }

        private static string FormatGenres(GenreRootResponse genreRootResponse, UpcomingMovieResponse upcomingMovieResponse) {
            var listOfGenres = new List<string>();

            if (genreRootResponse != null && genreRootResponse.Genres.Count > 0) {
                foreach (var genreId in upcomingMovieResponse.GenreIds) {
                    var genre = genreRootResponse.Genres.FirstOrDefault(x => x.Id == genreId);

                    if (genre != null)
                        listOfGenres.Add(genre.Name);
                }
            }

            return string.Join(", ", listOfGenres);
        }
    }
}
