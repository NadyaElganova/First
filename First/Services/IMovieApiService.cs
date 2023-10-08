using First.Models;

namespace First.Services
{
    public interface IMovieApiService
    {
        Task<MovieApiResponse> SearchByTitleAsync(string title);
        Task<Movie> SearchByIdAsync(string id);
    }
}