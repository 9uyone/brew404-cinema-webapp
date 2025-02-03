using System.Text.Json;
using System.Text.Json.Nodes;
using BusinessLogic.DTOs;
using BusinessLogic.TMDbService;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.TMDbServise
{
	public class TMDbApiService
	{
		private HttpClient _client;
		private readonly string _apiKey;
		IConfiguration _configuration;

		public TMDbApiService(IConfiguration configuration)
		{
			_configuration = configuration;
			_client = new HttpClient();
			_apiKey = _configuration["TmdbApiKey"];
		}

		public async Task<JsonObject?> GetAsync(string endPoint, string query = "")
		{
			var url = $"{endPoint}?api_key={_apiKey}{query}";
			var responce = await _client.GetAsync(url);
			responce.EnsureSuccessStatusCode();

			var content =  await responce.Content.ReadAsStringAsync();
			JsonObject? jsonObject = JsonSerializer.Deserialize<JsonObject>(content
				, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			
			return jsonObject;
		}

		public async Task<List<MovieDTO>?> GetMovies(string query , int pageNum = 1)
		{
			JsonObject? jsonObj = await GetAsync(TmdbEndpoints.MoviesEnd(), TmdbEndpoints.MovieQuery(query,pageNum));
			if (jsonObj == null)
				return null;

			JsonArray? jsonArray = jsonObj["results"]?.AsArray();

			if (jsonArray == null)
				return null;

			List<MovieDTO>? result = JsonSerializer.Deserialize<List<MovieDTO>>(jsonArray.ToString());

			return result;
		}

		public async Task<List<ActorDTO>?> GetActors(int movieId, int count = 10)
		{
			JsonObject? jsonObj = await GetAsync(TmdbEndpoints.MovieCredits(movieId));
			if (jsonObj == null)
				return null;
			JsonArray? jsonArray = jsonObj["cast"]?.AsArray();

			if (jsonArray == null)
				return null;

			List<ActorDTO>? result = JsonSerializer.Deserialize<List<ActorDTO>>(jsonArray.ToString());

			return result?.Select(actor =>
			{
				actor.AvatarUrl = !string.IsNullOrEmpty(actor.AvatarUrl)
				? $"{TmdbEndpoints.SubImgUrl}{actor.AvatarUrl}"
				: string.Empty;

				return actor;
			})
			.Take(count)
			.ToList();
		}

		public async Task<MovieDTO?> GetMovieDetails(int movieId)
		{
			JsonObject? jsonObj = await GetAsync(TmdbEndpoints.MovieDetails(movieId));
			if (jsonObj == null)
				return null;

			MovieDTO? result = JsonSerializer.Deserialize<MovieDTO>(jsonObj.ToString());
			if (result != null)
			{
				result.ImageUrl = $"{TmdbEndpoints.SubImgUrl}{result.ImageUrl}";
				result.BackgroundUrl = $"{TmdbEndpoints.SubBackGroundUrl}{result.BackgroundUrl}";
			}
			return result;
		}

		public async Task<string?> GetTrailer(int movieId)
		{
			JsonObject? jsonObj = await GetAsync(TmdbEndpoints.MovieTrailer(movieId));
			if (jsonObj == null)
				return null;

			var trailers = jsonObj?["results"]?.AsArray()
				.Where(r => r["type"]?.ToString() == "Trailer")
				.ToList();

			if (trailers == null || !trailers.Any())
				return null;

			var trailerKey = trailers.First()?["key"]?.ToString();
			if (trailerKey == null)
				return null;

			string result = TmdbEndpoints.SubTrailerUrl + trailerKey;
			return result;
		}

		public async Task<List<GenreDTO>?> GetGenres()
		{
			JsonObject? jsonObj = await GetAsync(TmdbEndpoints.Genres);

			if (jsonObj == null)
				return null;

			JsonArray? jsonArray = jsonObj["genres"]?.AsArray();

			if (jsonArray == null)
				return null;

			return JsonSerializer.Deserialize<List<GenreDTO>>(jsonArray.ToString());
		}
	}
}


