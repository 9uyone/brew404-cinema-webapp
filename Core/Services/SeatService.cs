using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class SeatService
	{
		private readonly IRepository<Seat> _seatRepository;
		private readonly IRepository<Session> _sessionRepository;

		public SeatService(IRepository<Seat> seatRepository,
			IRepository<Session> sessionRepository)
		{
			_seatRepository = seatRepository;
			_sessionRepository = sessionRepository;
		}

		public async Task<int> GetSeatIdFromSessionIdByRowAndCol(int sessionId, int row, int col)
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

			return seat.Id;
		}

	}
}