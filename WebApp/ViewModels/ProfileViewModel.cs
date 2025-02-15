using BusinessLogic.DTOs;

namespace WebApp.ViewModels
{
	public class ProfileViewModel
	{
		public string Name { get; set; } = null!;
		public List<TicketDTO>? PastTickets { get; set; }
		public List<TicketDTO>? CurrentTickets { get; set; }

	}
}
