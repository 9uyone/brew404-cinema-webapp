using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services
{
	public class SessionService
	{
		private readonly IRepository<Session> _sessionRepository;
		private readonly IRepository<Movie> _moviesRepository;
		private readonly IRepository<Hall> _hallRepository;
		private readonly IMapper _mapper;

		public SessionService(IMapper mapper,
			IRepository<Session> sessionRepository,
			IRepository<Movie> moviesRepository,
			IRepository<Hall> hallRepository)
		{
			_mapper = mapper;
			_sessionRepository = sessionRepository;
			_hallRepository = hallRepository;
			_moviesRepository = moviesRepository;
		}

		public async Task<IEnumerable<SessionDTO>> GetAllSessionAsync()
		{
			var sessions = await Task.Run(() => _sessionRepository.Get(includeProperties: "Movie,Hall"));
			return _mapper.Map<List<SessionDTO>>(sessions);
		}

		public async Task<SessionDTO?> GetSessionByIdAsync(int id)
		{
			var session = await _sessionRepository.GetByID(id, includeProperties: "Movie,Hall");
			return session == null ? null : _mapper.Map<SessionDTO>(session);
		}

		public async Task<bool> AddSessionAsync(SessionDTO sessionDTO)
		{
			var session = _mapper.Map<Session>(sessionDTO);

			session.Movie = await _moviesRepository.GetByID(session.MovieId);
			session.Hall = await _hallRepository.GetByID(session.HallId);
			
			session.EndTime = session.StartTime + TimeSpan.FromMinutes(session.Movie.RunTime);
			var existingSessions = await _sessionRepository.Get(s => s.HallId == session.HallId);

			bool isOverlapping = existingSessions.Any(s =>
				(session.StartTime >= s.StartTime && session.StartTime < s.EndTime) ||
				(session.EndTime > s.StartTime && session.EndTime <= s.EndTime) ||
				(session.StartTime <= s.StartTime && session.EndTime >= s.EndTime));

			if (isOverlapping)
			{
				return false;
			}

			await _sessionRepository.Insert(session);
			return true;
		}

		public async Task UpdateSessionAsync(Session sessionDTO)
		{
			var session = _mapper.Map<Session>(sessionDTO);
			await _sessionRepository.Update(session);
		}

		public async Task DeleteSessionAsync(int id)
		{
			await _sessionRepository.Delete(id);
		}
	}
}