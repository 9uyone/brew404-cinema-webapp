using BusinessLogic.DTOs;
using BusinessLogic.Services;
using BusinessLogic.TMDbServise;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Route("admin/Movies")]
	public class MoviesController : Controller
	{
		private readonly TMDbApiService _tMDbApiService;
		private readonly MovieService _movieService;

		public MoviesController(MovieService movieService)
		{
			_tMDbApiService = new TMDbApiService();
			_movieService = movieService;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			return View(await _movieService.GetAllMoviesAsync());
		}

		[HttpGet("SearchMovies")]
		public IActionResult SearchMovies()
		{
			return View();
		}

		[HttpGet("Search")]
		public async Task<IActionResult> Search(string query)
		{
			var movies = await _tMDbApiService.GetMovies(query);
			return View(nameof(SearchMovies), movies);
		}

		[HttpGet("MovieDetails")]
		public async Task<IActionResult> MovieDetails(int id)
		{
			MovieDTO? movie = await _tMDbApiService.GetMovieDetails(id);
			if (movie != null)
			{
				movie.Actors = await _tMDbApiService.GetActors(movie.Id);
				movie.TrailerUrl = await _tMDbApiService.GetTrailer(movie.Id);
			}
			return View(movie);
		}

		public async Task<IActionResult> AddMovie(int id)
		{
			MovieDTO? movie = await _tMDbApiService.GetMovieDetails(id);
			if (movie == null)
				NotFound();

			movie.Actors = await _tMDbApiService.GetActors(movie.Id);
			movie.TrailerUrl = await _tMDbApiService.GetTrailer(movie.Id);

			await _movieService.AddMovieAsync(movie);

			return RedirectToAction(nameof(SearchMovies));
		}

	}
}
