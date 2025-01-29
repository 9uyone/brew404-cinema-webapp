using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Services
{
	public class MovieService
	{
		private readonly IMapper _mapper;
		private readonly IRepository<Movie> _movieRepository;
		private readonly IRepository<Genre> _genreRepository;
		private readonly IRepository<Actor> _actorRepository;

		public MovieService(IMapper mapper
			, IRepository<Movie> movieRepository
			, IRepository<Genre> genreRepository
			, IRepository<Actor> actorRepository)
		{
			_mapper = mapper;
			_movieRepository = movieRepository;
			_genreRepository = genreRepository;
			_actorRepository = actorRepository;
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

		private async Task <List<T>> GetExistingItems<T>(IRepository<T> repository ,List<int> indexes) where T : class, IEntity
		{
			var allItems = await repository.Get(tracking: true);
			var existingItems = allItems.Where(g => indexes.Contains(g.Id)).ToList();
			return existingItems.ToList();
		}

		public async Task AddMovieAsync(MovieDTO movieDTO)
		{
			var movie = _mapper.Map<Movie>(movieDTO);

			var genreIds = movieDTO.Genres?.Select(g => g.Id).ToList() ?? new List<int>();
			var existingGenres = await GetExistingItems(_genreRepository, genreIds);
			//var newGenres = movieDTO.Genres?.Where(g => !existingGenres.Any(e => e.Id == g.Id)).ToList() ?? new List<Genre>();

			var actorIds = movieDTO.Actors?.Select(a => a.Id).ToList() ?? new List<int>();
			var existingActors = await GetExistingItems(_actorRepository, actorIds);
			var newActors = _mapper.Map<List<Actor>>(movie.Actors?.Where(a => !existingActors.Any(ex => ex.Id == a.Id)).ToList());

			movie.Genres = existingGenres;
			var combinedActors = existingActors.Concat(newActors).ToList();
			movie.Actors = combinedActors;

			await _movieRepository.Insert(movie);
		} 

		public async Task SaveGenres(List<GenreDTO> genresDTO)
		{
			var genres = _mapper.Map<List<Genre>>(genresDTO);
			await _genreRepository.AddRange(genres);
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