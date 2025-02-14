using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	[ApiController]
	[Route("api/tickets/[action]")]
	[Authorize]
	public class TicketsController : Controller
	{
		private readonly TicketService _ticketService;
		private readonly SeatService _seatService;

		public TicketsController(TicketService ticketService, SeatService seatService)
		{
			_ticketService = ticketService;
			_seatService = seatService;
		}

		[HttpPost]
		public async Task<IActionResult> Create(TicketDTO ticketDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _ticketService.AddTicketAsync(ticketDTO);
			if (!result)
				return Conflict("Помилка створення квитка");

			return Ok("Квиток успішно створений");
		}

		[HttpPost]
		public async Task<IActionResult> CreateByElements(int sessionId, string userId, [FromBody]List<Tuple<int, int>> seats)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (await _seatService.IsAnySeatOcuupied(sessionId, seats))
			{
				return Conflict("{\"Деякі місця вже зайняті\"}");
			}

			var ticketDTOs = new List<TicketDTO>();

			foreach (var seat in seats)
			{
				var seatObj =  await _seatService.GetSeatIdFromSessionIdByRowAndCol(sessionId, seat.Item1, seat.Item2);
				var seatId = seatObj?.Id; 
				if (seatId == null)
				{
					return Conflict("{\"Не вдалося знайти місце: ряд " + seat.Item1 + ", місце " + seat.Item2 + "\"}");
				}
				ticketDTOs.Add(new TicketDTO
				{
					UserId = userId,
					SessionId = sessionId,
					SeatId = seatId.Value
				});
			}

			//var ticketDTOs = seats.Select(s => new TicketDTO
			//{
			//	UserId = userId,
			//	SessionId = sessionId,
			//	SeatId = _seatService.GetSeatIdFromSessionIdByRowAndCol(sessionId, s.Item1, s.Item2).Id
			//});

			if (await _ticketService.AddTicketsAsync(ticketDTOs))
				return Ok();
			else return Conflict("{\"Помилка створення квитків\"}");
		}
	}
}