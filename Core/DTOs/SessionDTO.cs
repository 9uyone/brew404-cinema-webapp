using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
	public class SessionDTO
	{
		public int Id { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public MovieDTO? Movie { get; set; }
		public int MovieId { get; set; }

		public int HallId { get; set; }
		public HallDTO? Hall { get; set; }

	}
}
