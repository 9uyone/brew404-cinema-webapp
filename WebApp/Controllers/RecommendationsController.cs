using BusinessLogic.Services;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	[Authorize]
	public class RecommendationsController : Controller
	{
		private readonly AccountService _accountService;
		private readonly MovieService _movieService;
		private readonly UserManager<User> _userManager;

		public RecommendationsController(AccountService accountService
			, MovieService movieService
			, UserManager<User> userManager)
		{
			_accountService = accountService;
			_movieService = movieService;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			var genres = await _accountService.FavMovieGenres(user?.Id);
			var movies = await _movieService.GetRecomendedMovies(genres);

			return View(movies);
		}
	}
}
