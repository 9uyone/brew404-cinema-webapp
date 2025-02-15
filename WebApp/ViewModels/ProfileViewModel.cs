using BusinessLogic.DTOs;

namespace WebApp.ViewModels
{
	public class ProfileViewModel
	{
		public string Name { get; set; } = null!;
		public List<TicketDTO>? Tickets { get; set; }

	}
}
