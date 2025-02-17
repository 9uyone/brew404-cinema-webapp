using BusinessLogic.DTOs;

namespace WebApp.ViewModels
{
	public class MoviesAndPremieres
	{
		public List<MovieDTO> Movies { get; set; }
		public List<MovieDTO> Premieres { get; set; }
	}
}