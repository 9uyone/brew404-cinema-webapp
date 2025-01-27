using BusinessLogic.DTOs;
using BusinessLogic.TMDbService;
using BusinessLogic.TMDbServise;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class AdminController : Controller
	{
		TMDbApiService service;

		public AdminController()
		{
			service = new TMDbApiService();
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SearchMovies()
		{
			return View();
		}

		public async Task<IActionResult> Search(string query)
		{
			var movies = await service.GetMovies(query);
			return View(nameof(SearchMovies), movies);
		}

		//public async Task<IActionResult> AddMovie()

		public async Task<IActionResult> MovieDetails(int id)
		{
			MovieDTO? movie = await service.GetMovieDetails(id);
			if(movie != null)
			{
				movie.Crew = await service.GetCrew(movie.Id);
				movie.TrailerUrl = await service.GetTrailer(movie.Id);
				Console.WriteLine(await service.GetAsync(TmdbEndpoints.MovieDetails(movie.Id)));
			}
			return View(movie);
		}
	}
}
