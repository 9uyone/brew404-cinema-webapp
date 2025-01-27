using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class SessionService
	{
		private readonly IRepository<Session> _sessionRepository;

		public SessionService(IRepository<Session> sessionRepository)
		{
			_sessionRepository = sessionRepository;
		}

		public async Task<IEnumerable<Session>> GetAllGenresAsync()
		{
			return await Task.Run(() => _sessionRepository.Get(includeProperties: "Movie,Hall"));
		}

		public async Task AddSessionAsync(Session session)
		{
			await _sessionRepository.Insert(session);
		}

		public async Task UpdateSessionAsync(Session session)
		{
			await _sessionRepository.Update(session);
		}

		public async Task DeleteSessionAsync(int id)
		{
			await _sessionRepository.Delete(id);
		}
	}
}