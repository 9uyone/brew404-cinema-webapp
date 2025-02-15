using BusinessLogic.DTOs;
using BusinessLogic.Services;
using BusinessLogic.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using WebApp.ViewModels;

namespace WebApp.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	[Area("admin")]
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

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _sessionService.GetAllSessionAsync());
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var addSessionViewModel = new AddSessionViewModel()
			{
				Movies = await _movieService.GetAllMoviesAsync(),
				Halls = await _hallService.GetAllHallsAsync()
			};

			return View(addSessionViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddSessionViewModel model)
		{
			if (model == null || model.MovieId == 0 || model.HallId == 0 || model.StartTime == DateTime.MinValue)
				return BadRequest("Недійсні дані");

			var sessionDTO = new SessionDTO()
			{
				MovieId = model.MovieId,
				HallId = model.HallId,
				StartTime = model.StartTime,
				Price = model.Price
			};

			if (TryValidateModel(sessionDTO) == false)
				return BadRequest(ModelState);

			bool isAdded = await _sessionService.AddSessionAsync(sessionDTO);

			if (!isAdded)
				return BadRequest("У цей час у цьому залі вже є сеанс!");
			
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _sessionService.DeleteSessionAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
