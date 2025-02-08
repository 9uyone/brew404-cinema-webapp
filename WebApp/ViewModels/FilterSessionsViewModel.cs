using BusinessLogic.DTOs;

namespace WebApp.ViewModels
{
	public class FilterSessionsViewModel
	{
		public List<MovieDTO>? Movies { get; set; }
		public List<SessionDTO>? Sessions { get; set; }
	}
}
