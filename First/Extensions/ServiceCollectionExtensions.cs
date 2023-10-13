using First.Options;
using First.Services;

namespace First.Extensions
{
    //Extension ->
    //1. class static
    //2. method static
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieService(this IServiceCollection services, Action<MovieApiOptions> options)
        {
            services.AddTransient<IMovieApiService, MovieApiService>();
            services.Configure(options);
            return services;
        }
    }
}
