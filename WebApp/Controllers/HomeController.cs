using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Diagnostics;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using WebApp.ViewModels;
using BusinessLogic.TMDbService;
using BusinessLogic.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		MovieService _movieService;
		SessionService _sessionService;
		HallService _hallService;

		public HomeController(MovieService movieService
			, SessionService sessionService
			, HallService hallService)
		{
			_movieService = movieService;
			_sessionService = sessionService;
			_hallService = hallService;
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

			if (movie == null)
			{
				return NotFound();
			}
			// active sessions
			var activeSessions = await _sessionService.GetAllSessionsByMovieIdAsync(movie.Id);
			var similar = (await _movieService.GetMoviesByGenres(movie.Genres))
				.Where(m => m.Id != movie.Id)
				.ToList();

			MovieDetailsViewModel movieDetailsViewModel = new MovieDetailsViewModel
			{
				Movie = movie,
				ActiveSessions = activeSessions,
				SimilarMovies = similar
			};

			return View(movieDetailsViewModel);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}