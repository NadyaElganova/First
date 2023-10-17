using First.Models;

namespace First.Services
{
    public interface IMovieApiService
    {
        Task<MovieApiResponse> SearchByTitleAsync(string title, int page=1);
        Task<Movie> SearchByIdAsync(string id);
    }
}