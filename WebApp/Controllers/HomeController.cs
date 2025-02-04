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

		public HomeController(MovieService movieService, SessionService sessionService)
		{
			_movieService = movieService;
			_sessionService = sessionService;
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

			MovieDetailsViewModel movieDetailsViewModel = new MovieDetailsViewModel
			{
				Movie = movie,
				ActiveSessions = activeSessions
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