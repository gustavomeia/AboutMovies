using AboutMovies.Interfaces;
using System;
using System.Collections.Generic;

namespace AboutMovies.Model
{
    public class UpcomingMovie : IRequestError
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public bool Error { get; set; }

        public string MessageErro { get; set; }

        public int StatusCode { get; set; }
    }
}
