using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toycloud.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Controllers
{
	[ApiController]
	[Route("api/tickets")]
	[Authorize]
	public class TicketsController : Controller
	{
		private readonly TicketService _ticketService;

		public TicketsController(TicketService ticketService)
		{
			_ticketService = ticketService;
		}

		[HttpGet("occupiedSeats/{sessionId}")]
		public async Task<IActionResult> GetOccupiedSeats(int sessionId)
		{
			var occupiedSeats = await _ticketService.GetOccupiedSeatsAsync(sessionId);
			return Ok(occupiedSeats);
		}

		[HttpPost]
		public async Task<IActionResult> CreateTicket([FromBodyOrDefault] TicketDTO ticketDTO, string jsonSeats)
		{
			Console.WriteLine(jsonSeats);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _ticketService.AddTicketAsync(ticketDTO);
			if (!result)
				return Conflict("Помилка створення квитка");

			return Ok("Ticket created successffully.");
		}

		[HttpPost]
		public async Task<IActionResult> CreateTicketsForSeatsByElements([FromBodyOrDefault] int sessionId, string UserId, List<Tuple<int, int>> RowsCols )
		{
			var occupiedSeats = await _ticketService.GetOccupiedSeatsAsync(sessionId);

			return null;
		}

	}
}