using BusinessLogic.DTOs;
using BusinessLogic.Services;
using BusinessLogic.TMDbService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	[Area("admin")]
	public class MoviesController : Controller {
		private readonly TMDbApiService _tMDbApiService;
		private readonly MovieService _movieService;

		public MoviesController(MovieService movieService, TMDbApiService tMDbApiService) {
			_tMDbApiService = tMDbApiService;
			_movieService = movieService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			Console.WriteLine(DateTime.Now);
			return View();
		}
	}
}
