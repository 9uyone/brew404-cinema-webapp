
namespace BusinessLogic.Property
{
	public  static class EnvProperty
	{
		public static string EnvFullPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "", ".env");
		public static string DbConnection = "DB_CONNECTION";
		public static string TmdbApiKey = "TMDB_API_KEY";
	}
}
