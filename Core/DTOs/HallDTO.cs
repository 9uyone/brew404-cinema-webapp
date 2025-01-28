using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
	public class HallDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string? Name { get; set; }

		[JsonPropertyName("total_seats")]
		public int TotalSeats { get; set; }
	}
}
