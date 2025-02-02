using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Diagnostics;
using BusinessLogic.DTOs;
using BusinessLogic.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
		MovieService _movieService;
		public HomeController(MovieService movieService)
        {
			_movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
			return View(await _movieService.GetAllMoviesAsync());
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public async Task<IActionResult> Details(int id)
		{
			MovieDTO? movie = await _movieService.GetMovieByIdAsync(id);
			return View(movie);
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

	

    }
}