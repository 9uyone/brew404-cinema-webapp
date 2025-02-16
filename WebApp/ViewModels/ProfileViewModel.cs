using BusinessLogic.DTOs;
using BusinessLogic.DTOs.User;
using DataAccess.EntityModels;

namespace WebApp.ViewModels
{
	public class ProfileViewModel
	{
		public UpdateUserDTO? UpdateUser { get; set; }
		public User User { get; set; } = null!;
		public List<TicketDTO>? PastTickets { get; set; }
		public List<TicketDTO>? CurrentTickets { get; set; }
	}
}
