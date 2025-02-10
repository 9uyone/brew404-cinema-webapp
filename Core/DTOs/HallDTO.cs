using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
	public class HallDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string? Name { get; set; }

		public int NumbOfRows { get; set; }
		public int SeatsPerRow { get; set; }

		public List<SeatDTO>? Seats { get; set; }
	}
}
