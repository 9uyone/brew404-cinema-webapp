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

		public IEnumerable<ActorDTO>? Actors { get; set; }

		[JsonPropertyName("genres")]
		public IEnumerable<GenreDTO>? Genres { get; set; }

		[JsonPropertyName("vote_average")]
		public float VoteAverage { get; set; }

		[JsonPropertyName("runtime")]
		public int RunTime { get; set; }
		
		public override string ToString()
		{
			return $"Tile:{Title}\n\noverview: {Overview}\n\nImageUrl: {ImageUrl}\n\nBackgroundUrl: {BackgroundUrl}\n\n" +
				$"TrailerUrl: {TrailerUrl}\n\nReleaseDate: {ReleaseDate}";
		}

		public override bool Equals(object? obj)
		{
			return obj is MovieDTO dto && Id == dto.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
