using BusinessLogic.DTOs;
using System.Text.RegularExpressions;

namespace WebApp.ViewModels
{
	public class MovieDetailsViewModel
	{
		public MovieDTO? Movie { get; set; }
		public List<SessionDTO>? ActiveSessions { get; set; }
		public List<MovieDTO>? SimilarMovies { get; set; }
			
		public Dictionary<DateTime, List<SessionDTO>> GetGroupedSessionsByDate()
		{
			if (ActiveSessions == null)
			{
				return new Dictionary<DateTime, List<SessionDTO>>();
			}

			return ActiveSessions
				.GroupBy(s => s.StartTime.Date) // за датою
				.ToDictionary(g => g.Key, g  => g.OrderBy(s => s.StartTime).ToList()); // Сортуємо за часом
		}
	}
}
