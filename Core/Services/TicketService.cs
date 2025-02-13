using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class TicketService
	{
		private IRepository<Ticket> _ticketRepository;
		private IMapper _mapper;

		public TicketService(IRepository<Ticket> ticketRepository,
			IRepository<Session> sessionRepository,
			IRepository<Seat> seatRepository,
			IMapper mapper)
		{
			_ticketRepository = ticketRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<TicketDTO?>> GetAllTicketAsync(int id)
		{
			var tickets = await Task.Run(() => _ticketRepository.Get(includeProperties: "User,Seat,Session"));
			return _mapper.Map<List<TicketDTO>>(tickets);
		}

		public async Task<TicketDTO?> GetTicketByIdAsync(int id)
		{
			var ticket = await _ticketRepository.GetByID(id, includeProperties: "User,Seat,Session");
			return ticket == null ? null : _mapper.Map<TicketDTO>(ticket);
		}

		public async Task<bool> AddTicketAsync(TicketDTO ticketDTO)
		{
			var ticket = _mapper.Map<Ticket>(ticketDTO);

			// Перевірка, чи зайнято
			var existingTickets = await _ticketRepository.Get(
				t => t.SeatId == ticketDTO.SeatId && t.SessionId == ticketDTO.SessionId);

			if (existingTickets.Any())
			{
				return false;
			}

			ticket.PurchaseTime = DateTime.Now;
			await _ticketRepository.Update(ticket);
			return true;
		}

		public async Task<bool> AddTicketsAsync(IEnumerable<TicketDTO> ticketDTOs)
		{
			try
			{
				if (!ticketDTOs.Any()) return false;

				var tickets = _mapper.Map<List<Ticket>>(ticketDTOs);

				// Перевіряємо чи всі необхідні поля заповнені
				if (tickets.Any(t => t.SessionId == 0 || t.SeatId == 0 || string.IsNullOrEmpty(t.UserId)))
				{
					return false;
				}

				// Перевіряємо чи є вже квитки для цих місць
				/*var sessionId = tickets.First().SessionId;
				var seatIds = tickets.Select(t => t.SeatId).ToList();

				var existingTickets = await _ticketRepository.Get(
					filter: t => t.SessionId == sessionId && seatIds.Contains(t.SeatId),
					tracking: true
				);

				if (existingTickets.Any())
				{
					return false;
				}*/

				// Встановлюємо час покупки
				var currentTime = DateTime.Now;
				foreach (var ticket in tickets)
				{
					ticket.PurchaseTime = currentTime;
				}

				await _ticketRepository.AddRange(tickets);
				return true;
			}
			catch (Exception ex)
			{
				// Логуємо помилку
				Console.WriteLine($"Помилка при додаванні квитків: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> DeleteTicketAsync(int id)
		{
			var existingTicket = await _ticketRepository.GetByID(id);
			if (existingTicket == null)
			{
				return false;
			}
			await _ticketRepository.Delete(id);
			return true;
		}

		public async Task<IEnumerable<TicketDTO>> GetTicketByUserIdAsync(string userId)
		{
			var tickets = await _ticketRepository.Get(
				filter: t => t.UserId == userId,
				includeProperties: "Seat,Session"
				);

			return _mapper.Map<List<TicketDTO>>(tickets);
		}
	}
}