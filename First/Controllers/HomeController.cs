using First.Models;
using First.Services;
using First.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace First.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieApiService movieApiService;
        public HomeController(IMovieApiService movieApiService)
        {
            this.movieApiService = movieApiService;
        }

        public IActionResult Index()
        {
            //ViewBag.name = "Nadya";
            //ViewBag.age = "28";
            return View();
        }
        public async Task<IActionResult> Movie(string id)
        {
            Movie movie = null;
            try
            {
                movie = await movieApiService.SearchByIdAsync(id);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
            }

            return View(movie);
        }
        public async Task<IActionResult> Search(string title, int page=1)
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
                searchViewModel.TotalPages = (int)Math.Ceiling(result.TotalResults / 10.0);
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