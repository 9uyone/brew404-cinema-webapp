using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class SeatService
	{
		private readonly IRepository<Seat> _seatRepository;
		private readonly IRepository<Session> _sessionRepository;
		private IRepository<Ticket> _ticketRepository;
		private readonly IMapper _mapper;

		public SeatService(IRepository<Seat> seatRepository,
			IRepository<Session> sessionRepository,
			IRepository<Ticket> ticketRepository,
			IMapper mapper)
		{
			_seatRepository = seatRepository;
			_sessionRepository = sessionRepository;
			_ticketRepository = ticketRepository;
			_mapper = mapper;
		}

		public async Task<SeatDTO> GetSeatIdFromSessionIdByRowAndCol(int sessionId, int row, int col)
		{
			var session = await _sessionRepository.GetByID(sessionId);
			if (session == null)
			{
				throw new ArgumentException($"Session with ID {sessionId} not found.");
			}

			var seats = await _seatRepository.Get(
				filter: s => s.Row == row && s.Number == col && s.HallId == session.HallId
			);

			var seat = seats.FirstOrDefault();
			if (seat == null)
			{
				throw new ArgumentException($"Seat with row {row} and number {col} not found in the hall for session {sessionId}.");
			}

			return _mapper.Map<SeatDTO>(seat);
		}

		public async Task<IEnumerable<Tuple<int, int>>> GetOccupiedSeatsAsync(int sessionId)
		{
			var tickets = await _ticketRepository.Get(
				filter: t => t.SessionId == sessionId,
				includeProperties: "Seat"
				);

			return tickets.Select(t => new Tuple<int, int>(t.Seat.Row, t.Seat.Number));
		}

		public async Task<bool> IsAnySeatOcuupied(int sessionId, List<Tuple<int, int>> seats)
		{
			foreach (var seat in seats)
			{
				var seatDTO = await GetSeatIdFromSessionIdByRowAndCol(sessionId, seat.Item1, seat.Item2);
				if (seatDTO != null)
				{
					if ((await _ticketRepository.Get(
						t => t.SessionId == sessionId && t.SeatId == seatDTO.Id
					)).Any())
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}