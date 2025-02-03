using BusinessLogic.DTOs;
using BusinessLogic.Services;
using BusinessLogic.TMDbServise;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Route("admin/[Controller]")]
	[Area("admin")]
	public class MoviesController : Controller {
		private readonly TMDbApiService _tMDbApiService;
		private readonly MovieService _movieService;

		public MoviesController(MovieService movieService, TMDbApiService tMDbApiService) {
			_tMDbApiService = tMDbApiService;
			_movieService = movieService;
		}

		// ... rest of the code remains unchanged
	}
}
