using System;

namespace AboutMovies.Model
{
    public class Movie
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string PosterUrl { get; set; }
    }
}
