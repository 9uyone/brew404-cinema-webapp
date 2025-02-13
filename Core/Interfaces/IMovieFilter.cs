using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
	public interface IMovieFilter
	{
		public Task<IEnumerable<MovieDTO>> GetFilteredMovies(MovieFilteredDTO filter);
	}
}
