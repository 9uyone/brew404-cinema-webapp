using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace WebApp.Controllers.Admin
{
	[Route("admin/[Controller]")]
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

		[HttpPost("add")]
		public async Task<IActionResult> AddHall(HallDTO hall)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _hallService.AddHallAsync(hall); 

			return RedirectToAction(nameof(Index));
		}

		[HttpPost("delete")]
		public async Task<IActionResult> DeleteHall(int Id)
		{
			await _hallService.DeleteHallAsync(Id);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateHall(HallDTO hall)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _hallService.UpdateHallAsync(hall);

			return RedirectToAction(nameof(Index));
		}
	}
}
