using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
	public class MovieDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("original_title")]
		public string? Title { get; set; }

		[JsonPropertyName("overview")]
		public string? Overview { get; set; }

		[JsonPropertyName("poster_path")]
		public string? ImageUrl { get; set; }

		[JsonPropertyName("backdrop_path")]
		public string? BackgroundUrl { get; set; }

		public string? TrailerUrl { get; set; }

		[JsonPropertyName("release_date")]
		public string? ReleaseDate { get; set; }

		public List<ActorDTO>? Actors { get; set; }
		

		[JsonPropertyName("genres")]
		public List<GenreDTO>? Genres { get; set; }


		[JsonPropertyName("runtime")]
		public int RunTime { get; set; }
		
		public override string ToString()
		{
			return $"Tile:{Title}\n\noverview: {Overview}\n\nImageUrl: {ImageUrl}\n\nBackgroundUrl: {BackgroundUrl}\n\n" +
				$"TrailerUrl: {TrailerUrl}\n\nReleaseDate: {ReleaseDate}";
		}
	}
}
