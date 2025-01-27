using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
	public class MovieService
	{
		private readonly IRepository<Movie> _movieRepository;

		public MovieService(IRepository<Movie> movieRepository)
		{
			_movieRepository = movieRepository;
		}

		public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
		{
			var movies = await Task.Run(() => _movieRepository.Get(orderBy: q => q.OrderBy(m => m.Title))); // Movie

			return movies.Select(m => new MovieDTO
			{
				Id = m.Id,
				Title = m.Title,
				Overview = m.Overview,
				ImageUrl = m.ImageUrl,
				BackgroundUrl = m.BackgroundUrl,
				TrailerUrl = m.TrailerUrl,
				ReleaseDate = m.ReleaseDate.ToString("yyyy-MM-dd"),
				Genres = m.Genres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name }).ToList(),
				Crew = m.Credits.Select(c => new CrewMemberDTO { Id = c.Id, Name = c.Name, AvatarUrl = c.AvatarUrl }).ToList()
			});
		}

		public async Task<MovieDTO?> GetMovieByIdAsync(int id)
		{
			var movie = await _movieRepository.GetByID(id);
			if (movie == null) return null;

			return new MovieDTO
			{
				Id = movie.Id,
				Title = movie.Title,
				Overview = movie.Overview,
				ImageUrl = movie.ImageUrl,
				BackgroundUrl = movie.BackgroundUrl,
				TrailerUrl = movie.TrailerUrl,
				ReleaseDate = movie.ReleaseDate.ToString("yyyy-MM-dd"),
				Genres = movie.Genres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name }).ToList(),
				Crew = movie.Credits.Select(c => new CrewMemberDTO { Id = c.Id, Name = c.Name, AvatarUrl = c.AvatarUrl }).ToList()
			};
		}

		public async Task AddMovieAsync(MovieDTO movieDTO)
		{
			var movie = new Movie
			{
				Id = movieDTO.Id,
				Title = movieDTO.Title,
				Overview = movieDTO.Overview,
				ImageUrl = movieDTO.ImageUrl,
				BackgroundUrl = movieDTO.BackgroundUrl,
				TrailerUrl = movieDTO.TrailerUrl,
				ReleaseDate =DateTime.Parse(movieDTO.ReleaseDate),
				Genres = movieDTO.Genres.Select(g => new Genre { Id = g.Id, Name = g.Name }).ToList(),
				Credits = movieDTO.Crew.Select(c => new Credit { Id = c.Id, Name = c.Name, AvatarUrl = c.AvatarUrl }).ToList()
			};

			await _movieRepository.Insert(movie);
		}

		public async Task UpdateMovieAsync(MovieDTO movieDTO)
		{
			var movie = new Movie
			{
				Id = movieDTO.Id,
				Title = movieDTO.Title,
				Overview = movieDTO.Overview,
				ImageUrl = movieDTO.ImageUrl,
				BackgroundUrl = movieDTO.BackgroundUrl,
				TrailerUrl = movieDTO.TrailerUrl,
				ReleaseDate = DateTime.Parse(movieDTO.ReleaseDate),
				Genres = movieDTO.Genres.Select(g => new Genre { Id = g.Id, Name = g.Name }).ToList(),
				Credits = movieDTO.Crew.Select(c => new Credit { Id = c.Id, Name = c.Name, AvatarUrl = c.AvatarUrl }).ToList()
			};

			await _movieRepository.Update(movie);
		}

		public async Task Delete(int id)
		{
			await _movieRepository.Delete(id);
		}
	}
}