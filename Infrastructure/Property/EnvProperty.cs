
namespace BusinessLogic.Property
{
	public  static class EnvProperty
	{
		public static string EnvFullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..")) + "\\.env";
		public static string DbConnection = "DB_CONNECTION";
		public static string TmdbApiKey = "TMDB_API_KEY";
	}
}
