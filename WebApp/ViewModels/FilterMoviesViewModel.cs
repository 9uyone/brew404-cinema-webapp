using BusinessLogic.DTOs;

namespace WebApp.ViewModels
{
	public class FilterMoviesViewModel
	{
		public List<GenreDTO>? Genres { get; set; }
		public List<MovieDTO>? Movies { get; set; }
	}
}
