

using First.Models;
using First.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NuGet.ProjectModel;
using System.Text.Json;

namespace First.Services
{
    public class MovieApiService : IMovieApiService
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        private HttpClient httpClient { get; set; }
        private IMemoryCache memoryCache { get; }

        public MovieApiService(IHttpClientFactory httpClientFactory, IOptions<MovieApiOptions> options, IMemoryCache memoryCache)
        {
            BaseUrl = options.Value.BaseUrl;
            ApiKey = options.Value.ApiKey;

            httpClient = httpClientFactory.CreateClient();
            this.memoryCache = memoryCache;
        }
        public async Task<MovieApiResponse> SearchByTitleAsync(string title, int page = 1)
        {
            MovieApiResponse result;
            if (true)//tr && !memoryCache.TryGetValue(title.ToLower(),out result))
            {
                var response = await httpClient.GetAsync($"{BaseUrl}?s={title}&apikey={ApiKey}&page={page}");
                var json = await response.Content.ReadAsStringAsync();
                //result = JsonConvert.DeserializeObject<MovieApiResponse>(json);
                result = JsonSerializer.Deserialize<MovieApiResponse>(json);
                if (result.Response == "False")
                    throw new Exception(result.Error);

                var cacheTime = new MemoryCacheEntryOptions();
                cacheTime.SetAbsoluteExpiration(TimeSpan.FromDays(5));

                memoryCache.Set(title.ToLower(), result, cacheTime);
            }
            return result;
        }

        public async Task<Movie> SearchByIdAsync(string id)
        {
            Movie result;

            if (true)//!memoryCache.TryGetValue(id, out result))
            { 
                var response = await httpClient.GetAsync($"{BaseUrl}?&apikey={ApiKey}&i={id}");
                var json = await response.Content.ReadAsStringAsync();
                //result = JsonConvert.DeserializeObject<Movie>(json);
                result = JsonSerializer.Deserialize<Movie>(json);
                if (result.Response == "False")
                    throw new Exception(result.Error);

                //var cacheTime = new MemoryCacheEntryOptions();
                //cacheTime.SetAbsoluteExpiration(TimeSpan.FromDays(5));

                //memoryCache.Set(id, result);
            }
                                  
            return result;
        }
    }


}
