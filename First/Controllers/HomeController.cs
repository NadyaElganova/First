using First.Models;
using First.Services;
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
        public async Task<IActionResult> Search(string title)
        {
            // search movies

            //  HttpClient client = new HttpClient();
            //var response = await client.GetAsync($"https://omdbapi.com/?s={title}&apikey=5b8b7798");
            //var result = await  response.Content.ReadAsStringAsync();
            
            MovieApiResponse result = null;

            try
            {
                result = await movieApiService.SearchByTitleAsync(title);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
            }            

            //ViewBag.Result = result;
            ViewBag.searchMovie = title;
            return View(result);
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