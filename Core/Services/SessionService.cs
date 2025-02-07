using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
	public class SessionService : ISessionFilter
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

		public async Task<IEnumerable<SessionDTO>> GetFilteredSessions(SessionFilterDTO filter)
		{
			var sessionQuery = await _sessionRepository.Get(includeProperties: "Movie,Hall.Seats");

			if (filter.MovieId.HasValue)
				sessionQuery = sessionQuery.Where(session => session.MovieId == filter.MovieId);
			if (filter.Date.HasValue)
				sessionQuery = sessionQuery.Where(session => session.StartTime == filter.Date);

			sessionQuery = filter.SortBy?.ToLower() switch
			{
				"date" => filter.Descending ? sessionQuery.OrderByDescending(s => s.StartTime) : sessionQuery.OrderBy(s => s.StartTime),
				_ => sessionQuery
			};

			return _mapper.Map<List<SessionDTO>>(sessionQuery);
		}

		public async Task<SessionDTO?> GetSessionByIdAsync(int id)
		{
			var session = await _sessionRepository.GetByID(id, includeProperties: "Movie,Hall.Seats");
			return session == null ? null : _mapper.Map<SessionDTO>(session);
		}

		public async Task<List<SessionDTO>> GetAllSessionsByMovieIdAsync(int movieId)
		{
			var activeSessions = await _sessionRepository.Get(
				filter: s => s.MovieId == movieId,
				includeProperties: "Movie,Hall");

			return _mapper.Map<List<SessionDTO>>(activeSessions).ToList();
		}

		public async Task<Dictionary<DateTime, List<SessionDTO>>> GetGroupedSessionsAsync(int movieId)
		{
			var sessions = await _sessionRepository.Get(
				filter: s => s.MovieId == movieId,
				includeProperties: "Movie,Hall"
			);

			return sessions
				.GroupBy(s => s.StartTime.Date)
				.ToDictionary(
					g => g.Key,
					g => g.Select(s => _mapper.Map<SessionDTO>(s)).ToList()
				);
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