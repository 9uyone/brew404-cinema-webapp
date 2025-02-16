using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	[Area("admin")]
	public class PanelController : Controller
	{
		private readonly MovieService _movieService;
		private readonly TicketService _ticketService;

		public PanelController(MovieService movieService
			, TicketService ticketService)
		{
			_movieService = movieService;
			_ticketService = ticketService;
		}

		public async Task<IActionResult> Index()
		{
			var ticketDate = await _ticketService.GetTicketCountByMovieAsync();
			var topMovies = await _movieService.GetTopMoviesByTicketsAsync(ticketDate);
			return View(topMovies);
		}
	}
}
