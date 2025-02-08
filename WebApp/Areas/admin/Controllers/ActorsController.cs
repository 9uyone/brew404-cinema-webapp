using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	[Area("admin")]
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

		[HttpPost]
		public async Task<IActionResult> Add(ActorDTO actor)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _actorService.AddActorAsync(actor);
			return RedirectToAction(nameof(Index));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _actorService.DeleteActorAsync(id);
			return RedirectToAction(nameof(Index));
		}

		[HttpPut]
		public async Task<IActionResult> Update(ActorDTO actor)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _actorService.UpdateActorAsync(actor);
			return RedirectToAction(nameof(Index));
		}
	}
}
