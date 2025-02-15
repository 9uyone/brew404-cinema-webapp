using BusinessLogic.DTOs;
using BusinessLogic.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApp.DTOs;

namespace WebApp.Controllers
{
	[ApiController]
	[Route("api/tickets/[action]")]
	[Authorize]
	public class TicketsController : Controller
	{
		private readonly TicketService _ticketService;
		private readonly SeatService _seatService;
		private readonly IValidator<TicketDTO> _ticketDTOvalidator;

		public TicketsController(TicketService ticketService, 
			SeatService seatService, 
			IValidator<TicketDTO> ticketDTOvalidator)
		{
			_ticketService = ticketService;
			_seatService = seatService;
			_ticketDTOvalidator = ticketDTOvalidator;
		}

		/*[HttpPost]
		public async Task<IActionResult> Create(TicketDTO ticketDTO)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _ticketService.AddTicketAsync(ticketDTO);
			if (!result)
				return Conflict("Помилка створення квитка");

			return Ok();
		}*/

		[HttpPost]
		public async Task<IActionResult> Create(TicketsCreationDTO model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var ticketDTOs = new List<TicketDTO>();

			foreach (var seat in model.Seats)
			{
				int? seatId = null;
				try
				{
					seatId = (await _seatService.GetSeatIdFromSessionIdByRowAndCol(model.SessionId, seat.Item1, seat.Item2))?.Id;
				}
				catch {
					ModelState.AddModelError("Seats", $"Місце в ряду {seat.Item1} номер {seat.Item2} не знайдено в залі для сеансу {model.SessionId}.");
					return BadRequest(ModelState);
				}

				var ticketDTO = new TicketDTO
				{
					UserId = model.UserId,
					SessionId = model.SessionId,
					SeatId = seatId,
				};

				var validationResult = _ticketDTOvalidator.Validate(ticketDTO);
				if (!validationResult.IsValid) {
					var modelErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
					foreach (var error in modelErrors) {
						ModelState.AddModelError("Tickets", error);
					}
					return BadRequest(ModelState);
				}

				ticketDTOs.Add(ticketDTO);
			}

			if (await _ticketService.AddTicketsAsync(ticketDTOs))
				return Ok();
			else
			{
				ModelState.AddModelError("Tickets", "Помилка створення квитків");
				return Conflict(ModelState);
			}
		}
	}
}