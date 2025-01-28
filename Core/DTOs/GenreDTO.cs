using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
	public class GenreDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string? Name { get; set; }
	}
}
