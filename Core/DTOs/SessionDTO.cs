using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
	public class SessionDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("start_time")]
		public DateTime StartDate { get; set; }

		[JsonPropertyName("end_time")]
		public DateTime EndDate { get; set; }

		[JsonPropertyName("movie_id")]
		public int MovieId { get; set; }

		[JsonPropertyName("movie_title")]
		public string? MovieTitle { get; set; }

		[JsonPropertyName("hall_name")]
		public string? HallName { get; set; }
	}
}
