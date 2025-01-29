using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
	public class ActorDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string? Name { get; set; }

		[JsonPropertyName("character")]
		public string? Character { get; set; }

		[JsonPropertyName("profile_path")]
		public string? AvatarUrl { get; set; }
	}
}
