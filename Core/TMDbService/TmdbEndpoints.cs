
namespace BusinessLogic.TMDbService
{
	public static class TmdbEndpoints
	{
		public const string BaseUrl = "https://api.themoviedb.org/3/";

		public static string Genres => $"{BaseUrl}genre/movie/list";

		public static string MoviesEnd() => $"{BaseUrl}search/movie";
		public static string MovieQury(string query) => $"&query={query}";
		public static string MovieDetails(int movieId) => $"{BaseUrl}movie/{movieId}";
		public static string MovieTrailer(int movieId) => $"{BaseUrl}movie/{movieId}/videos";
		public static string MovieCredits(int movieId) => $"{BaseUrl}movie/{movieId}/credits";

	}
}
