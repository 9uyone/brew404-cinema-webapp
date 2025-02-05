using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	//[Route("admin/[Controller]")]
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

		[HttpPost("add")]
		public async Task<IActionResult> AddGenre(string name)
		{
			var genre = new GenreDTO { Name = name };
			await _genreService.AddGenreAsync(genre);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost("delete")]
		public async Task<IActionResult> DeleteGenre(int id)
		{
			await _genreService.DeleteGenreAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
