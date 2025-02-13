using BusinessLogic.DTOs;

namespace WebApp.ViewModels
{
	public class SessionDetailsViewModel
	{
		public SessionDTO Session { get; set; }
		public IEnumerable<Tuple<int, int>> OccupiedSeats { get; set; } = new List<Tuple<int, int>>();
	}
}