using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	[Area("admin")]
	public class HallController : Controller
	{
		private HallService _hallService;

		public HallController(HallService hallService)
		{
			_hallService = hallService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _hallService.GetAllHallsAsync());
		}

		[HttpPost]
		public async Task<IActionResult> Add(HallDTO hall)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _hallService.AddHallAsync(hall); 

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int Id)
		{
			await _hallService.DeleteHallAsync(Id);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Update(HallDTO hall)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _hallService.UpdateHallAsync(hall);

			return RedirectToAction(nameof(Index));
		}
	}
}
