using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	[ApiController]
	[Route("api/tickets")]
	public class TicketsController : ControllerBase
	{
		private readonly TicketService _ticketService;

		public TicketsController(TicketService ticketService)
		{
			_ticketService = ticketService;
		}

		[HttpGet("occupiedSeats/{sessionId}")]
		public async Task<IActionResult> GetOccutiedSeats(int sessionId)
		{
			var occupiedSeats = await _ticketService.GetOccupiedSeatsAsync(sessionId);
			return Ok(occupiedSeats);
		}

		[HttpPost]
		public async Task<IActionResult> CreateTicket([FromBody] TicketDTO ticketDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _ticketService.AddTicketAsync(ticketDTO);
			if (!result)
				return Conflict("Ticket could not be created (maybe seat already occupied).");

			return Ok("Ticket created successffully.");
		}
	}
}