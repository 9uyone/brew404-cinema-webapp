using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
	public class MovieService : IMovieFilter
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
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
			_genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
			_actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
		}

		public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
		{
			var movies = await _movieRepository.Get(
				includeProperties: "Actors,Genres",
				orderBy: q => q.OrderBy(m => m.ReleaseDate)
			) ?? new List<Movie>();

			return _mapper.Map<List<MovieDTO>>(movies);
		}

		public async Task<IEnumerable<MovieDTO>> GetFilteredMovies(MovieFilteredDTO filter)
		{
			var movieQuery = await _movieRepository.Get(includeProperties: "Genres,Actors");

			if (filter.GenreIds != null && filter.GenreIds.Any())
				movieQuery = movieQuery.Where(m => m.Genres.Any(g => filter.GenreIds.Contains(g.Id)));

			if(filter.ActorsIds != null && filter.ActorsIds.Any())
				movieQuery = movieQuery.Where(m => m.Actors.Any(a => filter.ActorsIds.Contains(a.Id)));

			if (filter.Year.HasValue)
				movieQuery = movieQuery.Where(m => m.ReleaseDate.Year == filter.Year.Value);


			movieQuery = filter.SortBy?.ToLower() switch
			{
				"title" => filter.Descending ? movieQuery.OrderByDescending(m => m.Title) : movieQuery.OrderBy(m => m.Title),
				"date" => filter.Descending ? movieQuery.OrderByDescending(m => m.ReleaseDate) : movieQuery.OrderBy(m => m.ReleaseDate),
				"rating" => filter.Descending ? movieQuery.OrderByDescending(m => m.VoteAverage) : movieQuery.OrderBy(m => m.VoteAverage),
				_ => movieQuery
			};

			return _mapper.Map<List<MovieDTO>>(movieQuery);

		}

		public async Task<IEnumerable<MovieDTO>?> GetMoviesByGenres(IEnumerable<GenreDTO>? genres)
		{
			if (genres == null) return null;

			var genresIds = genres.Select(g => g.Id).ToList();

			var movies = await _movieRepository.Get(includeProperties: "Genres");
			var filteredMovies = movies.Where(movie => movie.Genres.Any(g => genresIds.Contains(g.Id)));
			return _mapper.Map<List<MovieDTO>>(filteredMovies);
		}

		public async Task<MovieDTO?> GetMovieByIdAsync(int id)
		{
			var movie = await _movieRepository.GetByID(id,
				includeProperties: "Actors,Genres");	
			
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
			var newGenres = _mapper.Map<List<Genre>>(movie.Genres?.Where(g => !existingGenres.Any(e => e.Id == g.Id)).ToList());

			var actorIds = movieDTO.Actors?.Select(a => a.Id).ToList() ?? new List<int>();
			var existingActors = await GetExistingItems(_actorRepository, actorIds);
			var newActors = _mapper.Map<List<Actor>>(movie.Actors?.Where(a => !existingActors.Any(ex => ex.Id == a.Id)).ToList());

			var combineGenres = existingGenres.Concat(newGenres).ToList();
			movie.Genres = combineGenres;
			var combinedActors = existingActors.Concat(newActors).ToList();
			movie.Actors = combinedActors;
			movie.VoteAverage = MathF.Round(movie.VoteAverage, 1);

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
			await _movieRepository.Update(movie,
				new List<string> { "Title", "Overview", "ImageUrl", "BackgroundUrl", "TrailerUrl" });
		}

		public async Task DeleteMovieAsync(int id)
		{
			await _movieRepository.Delete(id);
		}
	}
}