using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
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

		public async Task<IEnumerable<SessionDTO>> GetAllSessionAsync(bool onlyFutureSessions = false)
		{
			var sessions = await _sessionRepository.Get(includeProperties: "Movie,Hall") ?? new List<Session>();
			if(onlyFutureSessions)
				sessions = sessions.Where(s => s.EndTime > DateTime.Now);
			return _mapper.Map<List<SessionDTO>>(sessions);
		}

		public async Task<IEnumerable<SessionDTO>> GetFilteredSessions(SessionFilterDTO filter, bool onlyFutureSessions = false)
		{
			var query = _sessionRepository.Query();

			query = query
				.Include(s => s.Movie)
				.Include(s => s.Hall);

			if (onlyFutureSessions)
				query = query.Where(s => s.EndTime > DateTime.Now);

			if (filter.MovieId.HasValue)
				query = query.Where(s => s.MovieId == filter.MovieId);

			if (filter.Date.HasValue)
				query = query.Where(s => s.StartTime.Date == filter.Date);

			query = filter.SortBy?.ToLower() switch
			{
				"date" => filter.Descending ? query.OrderByDescending(s => s.StartTime) : query.OrderBy(s => s.StartTime),
				_ => query
			};

			var sessionList = await query.ToListAsync();
			return _mapper.Map<List<SessionDTO>>(sessionList);
		}

		public async Task<SessionDTO?> GetSessionByIdAsync(int id)
		{
			var session = await _sessionRepository.GetByID(id, includeProperties: "Movie,Hall.Seats");
			return session is null ? null : _mapper.Map<SessionDTO>(session);
		}

		public async Task<List<SessionDTO>> GetAllSessionsByMovieIdAsync(int movieId, bool onlyFutureSessions = false)
		{
			var activeSessions = await _sessionRepository.Get(
				filter: s => s.MovieId == movieId,
				includeProperties: "Movie,Hall");

			if (onlyFutureSessions)
				activeSessions = activeSessions.Where(s => s.EndTime > DateTime.Now);

			return _mapper.Map<List<SessionDTO>>(activeSessions).ToList();
		}

		public async Task<Dictionary<DateTime, List<SessionDTO>>> GetGroupedSessionsAsync(int movieId, bool onlyFutureSessions = false)
		{
			var sessions = await _sessionRepository.Get(
				filter: s => s.MovieId == movieId,
				includeProperties: "Movie,Hall"
			);

			if (onlyFutureSessions)
				sessions = sessions.Where(s => s.EndTime > DateTime.Now);

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