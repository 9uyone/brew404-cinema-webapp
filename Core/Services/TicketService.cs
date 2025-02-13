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
			var tickets = _mapper.Map<List<Ticket>>(ticketDTOs);
			// Перевірка, чи зайнято
			var existingTickets = await _ticketRepository.Get(
				t => ticketDTOs.Any(td => td.SeatId == t.SeatId && td.SessionId == t.SessionId));
			if (existingTickets.Any())
			{
				return false;
			}
			/*foreach (var ticket in tickets)
			{
				ticket.PurchaseTime = DateTime.Now;
			}*/
			await _ticketRepository.AddRange(tickets);
			return true;
		}

		public async Task<bool> UpdateTicketAsync(int id, TicketDTO ticketDTO)
		{
			var existinTicket = await _ticketRepository.GetByID(id);
			if (existinTicket == null)
			{
				return false;
			}

			var updatedTicket = _mapper.Map<Ticket>(ticketDTO);
			await _ticketRepository.Update(updatedTicket);
			return true;
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