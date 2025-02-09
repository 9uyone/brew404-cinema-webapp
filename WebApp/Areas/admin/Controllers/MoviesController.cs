using BusinessLogic.DTOs;
using BusinessLogic.Services;
using BusinessLogic.TMDbService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	[Area("admin")]
	public class MoviesController : Controller
	{
		private readonly TMDbApiService _tmdbApiService;
		private readonly MovieService _movieService;

		public MoviesController(MovieService movieService, TMDbApiService tmdbApiService)
		{
			_tmdbApiService = tmdbApiService;
			_movieService = movieService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _movieService.GetAllMoviesAsync());
		}

		[HttpGet]
		public async Task<IActionResult> Search()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> SearchByQuery(string query)
		{
			var movies = await _tmdbApiService.GetMovies(query);
			return View("Search", movies);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			MovieDTO? movie = await _tmdbApiService.GetMovieDetails(id);
			if (movie != null)
			{
				movie.Actors = await _tmdbApiService.GetActors(movie.Id);
				movie.TrailerUrl = await _tmdbApiService.GetTrailer(movie.Id);
			}
			return View(movie);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int id)
		{
			MovieDTO? movie = await _tmdbApiService.GetMovieDetails(id);
			if (movie == null)
				NotFound();

			movie.Actors = await _tmdbApiService.GetActors(movie.Id);
			movie.TrailerUrl = await _tmdbApiService.GetTrailer(movie.Id);

			await _movieService.AddMovieAsync(movie);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _movieService.DeleteMovieAsync(id);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Update(MovieDTO movie)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _movieService.UpdateMovieAsync(movie);
			return RedirectToAction(nameof(Index));
		}
	}
}