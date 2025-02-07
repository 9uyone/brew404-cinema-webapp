using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Diagnostics;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
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
			var movie = await _movieService.GetMovieByIdAsync(id);
			if (movie == null)
			{
				return NotFound();
			}

			var activeSessions = await _sessionService.GetGroupedSessionsAsync(movie.Id);
			var similarMovies = await _movieService.GetMoviesByGenres(movie.Genres);

			var movieDetailsViewModel = new MovieDetailsViewModel
			{
				Movie = movie,
				GroupedSessions = activeSessions,
				SimilarMovies = similarMovies.Where(m => m.Id != movie.Id).ToList()
			};

			return View(movieDetailsViewModel);
		}
		//public async Task<IActionResult> GetSessions(DateTime date, int movieId)
		//{
		//	var sessions = await _sessionService.GetSessionsByDateAndMovieIdAsync(date, movieId);

		//	// If there are no sessions, return an empty array
		//	if (sessions == null)
		//	{
		//		return Json(new List<SessionDTO>());
		//	}

		//	return Json(sessions);
		//}

		public async Task<IActionResult> SessionDetails(int id)
		{
			SessionDTO? session = await _sessionService.GetSessionByIdAsync(id);
			return View(session);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}