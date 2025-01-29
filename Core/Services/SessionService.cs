using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class SessionService
	{
		private readonly IRepository<Session> _sessionRepository;
		private readonly IMapper _mapper;

		public SessionService(IMapper mapper,
			IRepository<Session> sessionRepository)
		{
			_mapper = mapper;
			_sessionRepository = sessionRepository;
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

		public async Task AddSessionAsync(SessionDTO sessionDTO)
		{
			var session = _mapper.Map<Session>(sessionDTO);
			await _sessionRepository.Insert(session);
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