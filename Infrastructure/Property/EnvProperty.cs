
namespace BusinessLogic.Property
{
	public  static class EnvProperty
	{
		//public static string EnvFullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..")) + "\\.env";
		//public static string EnvFullPath = "/Users/davidkacur/brew404-cinema-webapp/brew404-cinema-webapp/.env";
		public static string EnvFullPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "", ".env");
		public static string DbConnection = "DB_CONNECTION";
		public static string TmdbApiKey = "TMDB_API_KEY";
	}
}
