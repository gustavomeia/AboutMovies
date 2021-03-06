﻿using AboutMovies.Model;
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
                Succeeded = true,
                ErrorMessage = string.Empty,
            };
        }

        private static Movie Movie(UpcomingMovieResponse upcomingMovieResponse, GenreRootResponse genreRootResponse) {
            return new Movie {
                BackdropUrl = baseImageUrl + upcomingMovieResponse.BackdropPath,
                Name = upcomingMovieResponse.Title,
                Genre = FormatGenres(genreRootResponse, upcomingMovieResponse),
                Overview = upcomingMovieResponse.Overview,
                ReleaseDate = upcomingMovieResponse.ReleaseDate,
                PosterUrl = baseImageUrl + upcomingMovieResponse.PosterPath,
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
