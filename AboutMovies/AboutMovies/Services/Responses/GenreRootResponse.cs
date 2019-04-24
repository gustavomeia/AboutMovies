using Newtonsoft.Json;
using System.Collections.Generic;

namespace AboutMovies.Services.Responses {
    public class GenreRootResponse {
        [JsonProperty("genres")]
        public List<GenreResponse> Genres { get; set; }

        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }
    }
}
