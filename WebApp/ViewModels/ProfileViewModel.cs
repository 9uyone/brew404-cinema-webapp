using BusinessLogic.DTOs;
using DataAccess.EntityModels;

namespace WebApp.ViewModels
{
	public class ProfileViewModel
	{
		public User User { get; set; } = null!;
		public List<TicketDTO>? PastTickets { get; set; }
		public List<TicketDTO>? CurrentTickets { get; set; }
	}
}
