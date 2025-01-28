using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services
{
	public class MovieService
	{
		private readonly IMapper _mapper;
		private readonly IRepository<Movie> _movieRepository;

		public MovieService(IMapper mapper
			, IRepository<Movie> movieRepository)
		{
			_mapper = mapper;
			_movieRepository = movieRepository;
		}

		public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
		{
			var movies = await _movieRepository.Get(
				includeProperties: "Actors,Genres",
				orderBy: q => q.OrderBy(m => m.ReleaseDate)
			);

			return _mapper.Map<List<MovieDTO>>(movies);
		}

		public async Task<MovieDTO?> GetMovieByIdAsync(int id)
		{
			var movie = await _movieRepository.GetByID(id,
				includeProperties: "Actors,Genres");			
			if (movie == null) return null;

			return _mapper.Map<MovieDTO>(movie);
		}

		public async Task AddMovieAsync(MovieDTO movieDTO)
		{
			var movie = _mapper.Map<Movie>(movieDTO);
			await _movieRepository.Insert(movie);
		}

		public async Task UpdateMovieAsync(MovieDTO movieDTO)
		{
			var movie = _mapper.Map<Movie>(movieDTO);
			await _movieRepository.Update(movie);
		}

		public async Task Delete(int id)
		{
			await _movieRepository.Delete(id);
		}
	}
}