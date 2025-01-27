using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class GenreService
	{
		private readonly IRepository<Genre> _genreRepository;

		public GenreService(IRepository<Genre> genreRepository)
		{
			_genreRepository = genreRepository;
		}

		public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
		{
			var genres = await Task.Run(() => _genreRepository.Get(orderBy: q => q.OrderBy(g => g.Name)));
			return genres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name });
		}

		public async Task AddGenreAsync(GenreDTO genreDTO)
		{
			var genre = new Genre { Name = genreDTO.Name };
			await _genreRepository.Insert(genre);
		}

		public async Task UpdateGenreAsync(GenreDTO genreDTO)
		{
			var genre = new Genre { Id = genreDTO.Id, Name = genreDTO.Name };
			await _genreRepository.Update(genre);
		}

		public async Task DeleteGenreAsync(int id)
		{
			await _genreRepository.Delete(id);
		}
	}
}
