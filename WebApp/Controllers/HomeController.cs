using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Diagnostics;
using BusinessLogic.TMDbServise;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using BusinessLogic.TMDbService;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
		TMDbApiService service = new TMDbApiService();
		MovieService _movieService;
		public HomeController(MovieService movieService)
        {
			_movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
			Console.WriteLine(await service.GetActors(335983));
			return View(await _movieService.GetAllMoviesAsync());
		}

        public async Task<IActionResult> Privacy()
        {
			List<ActorDTO>? res = await service.GetActors(335983);
			foreach(var el in res)
				Console.WriteLine(el.Name);
			return View();
        }

		public async Task<IActionResult> Details(int id)
		{
			//Console.WriteLine(await service.GetAsync(TmdbEndpoints.Genres));
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