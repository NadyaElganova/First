using First.Models;

namespace First.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }
    }
}
