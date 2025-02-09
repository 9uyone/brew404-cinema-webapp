using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
	public interface ISessionFilter
	{
		Task<IEnumerable<SessionDTO>> GetFilteredSessions(SessionFilterDTO filter);
	}
}
