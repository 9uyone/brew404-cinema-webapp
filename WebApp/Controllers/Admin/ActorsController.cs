using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Area("admin")]
	[Route("admin/[Controller]")]
	public class ActorsController : Controller
	{
		private readonly ActorService _actorService;

		public ActorsController(ActorService actorService)
		{
			_actorService = actorService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _actorService.GetAllActorsAsync());
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddActor(ActorDTO actor)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _actorService.AddActorAsync(actor);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost("delete")]
		public async Task<IActionResult> DeleteActor(int id)
		{
			await _actorService.DeleteActorAsync(id);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateActor(ActorDTO actor)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _actorService.UpdateActorAsync(actor);
			return RedirectToAction(nameof(Index));
		}

	}
}
