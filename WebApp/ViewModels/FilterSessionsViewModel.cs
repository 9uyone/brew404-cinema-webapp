using BusinessLogic.DTOs;

namespace WebApp.ViewModels
{
	public class FilterSessionsViewModel
	{
		public List<MovieDTO> Movies { get; set; } = new();
		public Dictionary<MovieDTO, List<SessionDTO>> GroupedSessions { get; set; } = new();
	}
}
