using First.Models;
using System.Collections.Concurrent;

namespace First.Services
{
    public class RecentMovieStorage : IRecentMovieStorage
    {
        public ConcurrentQueue<Movie> Movies = new ConcurrentQueue<Movie>();
        public void Add(Movie movie)
        {
            var result = Movies.FirstOrDefault(x => x.imdbID == movie.imdbID);
            if(result==null)
                Movies.Enqueue(movie);

            if(Movies.Count>6)
                Movies.TryDequeue(out Movie trash);
        }

        public IEnumerable<Movie> GetRecent()
        {
            return Movies.Take(6);
        }
    }
}
