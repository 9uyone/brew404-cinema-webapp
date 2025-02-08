using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Diagnostics;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		MovieService _movieService;
		SessionService _sessionService;
		GenreService _genreService;

		public HomeController(MovieService movieService
			, SessionService sessionService
			, GenreService genreService)
		{
			_movieService = movieService;
			_sessionService = sessionService;
			_genreService = genreService;
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

		public async Task<IActionResult> FilteredMovies(MovieFilteredDTO filter)
		{
			var movies = await _movieService.GetFilteredMovies(filter);
			var genres = await _genreService.GetAllGenresAsync();

			var filterMoviesViewModel = new FilterMoviesViewModel()
			{
				Movies = movies.ToList(),
				Genres = genres.ToList()
			};

			return View(filterMoviesViewModel);
		}

		public async Task<IActionResult> FilteredSessions(SessionFilterDTO filter)
		{
			var sessions = await _sessionService.GetFilteredSessions(filter);
			var movies = await _movieService.GetAllMoviesAsync();

			var filterSessionViewModel = new FilterSessionsViewModel()
			{
				Movies = movies.ToList(),
				Sessions = sessions.ToList()
			};

			return View(filterSessionViewModel);
		}

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