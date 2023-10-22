using First.Models;
using First.Services;
using First.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;

namespace First.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieApiService movieApiService;
        private readonly IRecentMovieStorage recentMovieStorage;
        
        public HomeController(IMovieApiService movieApiService, IRecentMovieStorage recentMovieStorage)
        {
            this.movieApiService = movieApiService;
            this.recentMovieStorage = recentMovieStorage;
        }

        public IActionResult Index()
        {
            //ViewBag.name = "Nadya";
            //ViewBag.age = "28";
            var result = recentMovieStorage.GetRecent();
            return View(result);
        }
        public async Task<IActionResult> Movie(string id)
        {
            Movie movie = null;
            try
            {
                movie = await movieApiService.SearchByIdAsync(id);
                recentMovieStorage.Add(movie);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
            }

            return View(movie);
        }
        public async Task<IActionResult> MovieModal(string id)
        {
            Movie movie = null;
            try
            {
                movie = await movieApiService.SearchByIdAsync(id);
                recentMovieStorage.Add(movie);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
            }

            return PartialView("_MovieModalPartial", movie);
        }
        public async Task<IActionResult> SearchResult(string title, int page = 1, int countViewPage = 4)
        {
            SearchViewModel searchViewModel = new SearchViewModel();

            try
            {
                MovieApiResponse result = await movieApiService.SearchByTitleAsync(title, page);
                searchViewModel.Movies = result.Cinemas;
            }
            catch (Exception ex)
            {
                searchViewModel.Error = ex.Message;
            }

            return PartialView("_MovieListPartial", searchViewModel.Movies);
        }

        public async Task<IActionResult> Search(string title, int page = 1, int startPage = 1, int endPage = 10)
        {
            // search movies

            //  HttpClient client = new HttpClient();
            //var response = await client.GetAsync($"https://omdbapi.com/?s={title}&apikey=5b8b7798");
            //var result = await  response.Content.ReadAsStringAsync();
            
            SearchViewModel searchViewModel = new SearchViewModel(); 
            try
            {
                MovieApiResponse result = await movieApiService.SearchByTitleAsync(title, page);
                //53   5.3

                searchViewModel.Title = title;
                searchViewModel.Movies = result.Cinemas;
                searchViewModel.Response = result.Response;
                searchViewModel.Error = result.Error;
                searchViewModel.CurrentPage = page;
                searchViewModel.TotalResults = result.TotalResults;
                searchViewModel.TotalPages = (int)Math.Ceiling(result.TotalResults / 10.0);

                if (searchViewModel.TotalPages > 10)
                {
                    if (searchViewModel.CurrentPage >= (endPage-4))
                    {
                        if (startPage != searchViewModel.TotalPages)
                        {
                            startPage = searchViewModel.CurrentPage;
                        }
                        endPage = searchViewModel.CurrentPage + 9;
                    }

                    if (endPage > searchViewModel.TotalPages)
                    {
                        endPage = searchViewModel.TotalPages;
                    }
                }

                ViewBag.startPage = startPage;
                ViewBag.endPage = endPage;
            }
            catch (Exception ex)
            {
                searchViewModel.Error = ex.Message;
            }            

            //ViewBag.Result = result;
            //ViewBag.searchMovie = title;
            return View(searchViewModel);
        }

        public IActionResult Privacy()
        {
 
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

