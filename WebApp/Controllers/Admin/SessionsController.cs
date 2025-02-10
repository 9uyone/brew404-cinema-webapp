using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers.Admin
{
	[Route("admin/[Controller]")]
	public class SessionsController : Controller
	{
		private readonly SessionService _sessionService;
		private readonly MovieService _movieService;
		private readonly HallService _hallService;
		public SessionsController(SessionService sessionService
			, MovieService movieService
			, HallService hallService)
		{
			_sessionService = sessionService;
			_movieService = movieService;
			_hallService = hallService;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			Console.WriteLine(DateTime.Now);
			return View(await _sessionService.GetAllSessionAsync());
		}

		[HttpGet("add")]
		public async Task<IActionResult> AddSession()
		{
			var addSessionViewModel = new AddSessionViewModel()
			{
				Movies = await _movieService.GetAllMoviesAsync(),
				Halls = await _hallService.GetAllHallsAsync()
			};

			return View(addSessionViewModel);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddSession(AddSessionViewModel model)
		{
			if (model == null || model.MovieId == 0 || model.HallId == 0 || model.StartTime == DateTime.MinValue)
				return BadRequest("Недійсні дані");


			var sessionDTO = new SessionDTO()
			{
				MovieId = model.MovieId,
				HallId = model.HallId,
				StartTime = model.StartTime
			};

			bool isAdded = await _sessionService.AddSessionAsync(sessionDTO);

			if (!isAdded)
				return BadRequest("У цей час у цьому залі вже є сеанс!");
			
			return RedirectToAction(nameof(Index));
		}

		[HttpPost("delete")]
		public async Task<IActionResult> DeleteSession(int id)
		{
			await _sessionService.DeleteSessionAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
