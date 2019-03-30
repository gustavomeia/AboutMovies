using Newtonsoft.Json;
using System.Collections.Generic;

namespace AboutMovies.Services.Responses {
    public class UpcomingMoviesRootResponse {
        [JsonProperty("results")]
        public List<UpcomingMovieResponse> Results { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}
