using System.Collections.Generic;

namespace AboutMovies.Model {
    public class UpcomingMovie {
        public List<Movie> Movies { get; set; } = new List<Movie>();

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }
    }
}
