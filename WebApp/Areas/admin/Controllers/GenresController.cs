using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	[Area("admin")]
	public class GenresController : Controller
	{
		private readonly GenreService _genreService;

		public GenresController(GenreService genreService)
		{
			_genreService = genreService;
		}

		public async Task<IActionResult> Index()
		{
			var genres = await _genreService.GetAllGenresAsync();
			return View(genres);
		}

		[HttpPost]
		public async Task<IActionResult> Add(string name)
		{
			var genre = new GenreDTO { Name = name };

			if (!TryValidateModel(genre))
				return BadRequest(ModelState);

			await _genreService.AddGenreAsync(genre);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _genreService.DeleteGenreAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
