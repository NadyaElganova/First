namespace First.Models
{
    public class MovieApiResponse
    {
        public Movie[] Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }
    }
}
