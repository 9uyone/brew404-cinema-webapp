
namespace BusinessLogic.TMDbService
{
	public static class TmdbEndpoints
	{
		public const string BaseUrl = "https://api.themoviedb.org/3/";

		public static string Genres => $"{BaseUrl}genre/movie/list";
		public static string SubImgUrl => "https://image.tmdb.org/t/p/w500";
		public static string SubTrailerUrl => "https://www.youtube.com/watch?v=";
		
		public static string MoviesEnd() => $"{BaseUrl}search/movie";
		public static string MovieQuery(string query, int pageNum) => $"&query={query}&page={pageNum}";
		public static string MovieDetails(int movieId) => $"{BaseUrl}movie/{movieId}";
		public static string MovieTrailer(int movieId) => $"{BaseUrl}movie/{movieId}/videos";
		public static string MovieCredits(int movieId) => $"{BaseUrl}movie/{movieId}/credits";
	}
}
