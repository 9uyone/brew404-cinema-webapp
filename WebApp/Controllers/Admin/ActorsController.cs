﻿using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Route("admin/Actors")]
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
			await _actorService.AddActorAsync(actor);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost("delete")]
		public async Task<IActionResult> DeleteActor(int id)
		{
			await _actorService.DeleteActorAsync(id);
			return RedirectToAction(nameof(Index));
		}

	}
}
