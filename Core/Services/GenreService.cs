using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class GenreService
	{
		private readonly IMapper _mapper;
		private readonly IRepository<Genre> _genreRepository;

		public GenreService(IMapper mapper,
			IRepository<Genre> genreRepository)
		{
			_mapper = mapper;
			_genreRepository = genreRepository;
		}

		public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
		{
			var genres = await Task.Run(() => _genreRepository.Get(orderBy: q => q.OrderBy(g => g.Name)));
			return _mapper.Map<List<GenreDTO>>(genres);
		}

		public async Task<GenreDTO?> GetGenreByIdAsync(int id)
		{
			var genre = await _genreRepository.GetByID(id);
			return genre == null ? null : _mapper.Map<GenreDTO?>(genre);
		}

		public async Task AddGenreAsync(GenreDTO genreDTO)
		{
			var genre = _mapper.Map<Genre>(genreDTO);
			await _genreRepository.Insert(genre);
		}

		public async Task UpdateGenreAsync(GenreDTO genreDTO)
		{
			var genre = _mapper.Map<Genre>(genreDTO);
			await _genreRepository.Update(genre);
		}

		public async Task DeleteGenreAsync(int id)
		{
			await _genreRepository.Delete(id);
		}
	}
}