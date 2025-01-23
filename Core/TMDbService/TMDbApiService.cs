using System.Text.Json;
using System.Text.Json.Nodes;
using BusinessLogic.Property;
using DotNetEnv;

namespace BusinessLogic.TMDbServise
{
	public class TMDbApiService
	{
		private HttpClient _client;
		private readonly string _apiKey;

		public TMDbApiService()
		{
			_client = new HttpClient();
			Env.Load(EnvProperty.EnvFullPath);
			_apiKey = Env.GetString(EnvProperty.TmdbApiKey);
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
	}
}
