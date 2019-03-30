using Newtonsoft.Json;

namespace AboutMovies.Services.Responses {
    public class GenreResponse {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
