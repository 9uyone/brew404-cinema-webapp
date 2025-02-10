using BusinessLogic.DTOs;

namespace WebApp.ViewModels
{
	public class AddSessionViewModel
	{
		public int MovieId { get; set; }
		public int HallId { get; set; }
		public DateTime StartTime { get; set; }

		public IEnumerable<MovieDTO>? Movies { get; set; }
		public IEnumerable<HallDTO>? Halls { get; set; }
	}
}
