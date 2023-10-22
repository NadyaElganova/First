using First.Models;

namespace First.Services
{
    public interface IRecentMovieStorage
    {
        void Add(Movie movie);
        IEnumerable<Movie> GetRecent();
    }
}
