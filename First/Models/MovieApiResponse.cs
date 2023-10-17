using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace First.Models
{
    public class MovieApiResponse
    {
        [JsonPropertyName("Search")]
        public Movie[] Cinemas { get; set; }

        [JsonPropertyName("totalResults")]
        public string TotalResultsString { get; set; }
        public int TotalResults { get => int.Parse(TotalResultsString); }
        public string Response { get; set; }
        public string Error { get; set; }

        //ctrl + r + r
    }
}
