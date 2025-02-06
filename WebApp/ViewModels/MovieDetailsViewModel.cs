using BusinessLogic.DTOs;
using System.Text.RegularExpressions;

namespace WebApp.ViewModels
{
	public class MovieDetailsViewModel
	{
		public MovieDTO? Movie { get; set; }
		public Dictionary<DateTime, List<SessionDTO>> GroupedSessions { get; set; }  // Груповані сеанси за датою
		public List<MovieDTO>? SimilarMovies { get; set; }

		// Додати метод для отримання сеансів для вибраної дати
		public List<SessionDTO> GetSessionsForSelectedDate(DateTime selectedDate)
		{
			return GroupedSessions.ContainsKey(selectedDate) ? GroupedSessions[selectedDate] : new List<SessionDTO>();
		}
	}
}