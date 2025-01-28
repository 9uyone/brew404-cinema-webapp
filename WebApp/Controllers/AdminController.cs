using BusinessLogic.DTOs;
using BusinessLogic.Services;
using BusinessLogic.TMDbService;
using BusinessLogic.TMDbServise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Controllers
{
	public class AdminController : Controller
	{
		TMDbApiService _apiService;
		MovieService _movieService;

		public AdminController(MovieService movieService)
		{
			_apiService = new TMDbApiService();
			_movieService = movieService;
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
			var movies = await _apiService.GetMovies(query);
			return View(nameof(SearchMovies), movies);
		}

		//public async Task<IActionResult> AddMovie()

		public async Task<IActionResult> MovieDetails(int id)
		{
			MovieDTO? movie = await _apiService.GetMovieDetails(id);
			if(movie != null)
			{
				movie.Actors = await _apiService.GetActors(movie.Id);
				movie.TrailerUrl = await _apiService.GetTrailer(movie.Id);
			}
			return View(movie);
		}

		public async Task<IActionResult> AddMovie(int id)
		{
			MovieDTO? movie = await _apiService.GetMovieDetails(id);
			if (movie == null)
				NotFound();

			movie.Actors = await _apiService.GetActors(movie.Id);
			movie.TrailerUrl = await _apiService.GetTrailer(movie.Id);

			await _movieService.AddMovieAsync(movie);

			return RedirectToAction(nameof(SearchMovies));
		}
	}
}
