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
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
		}

		public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
		{
			var genres = await _genreRepository.Get() ?? new List<Genre>();
			return _mapper.Map<List<GenreDTO>>(genres);
		}

		public async Task<GenreDTO?> GetGenreByIdAsync(int id)
		{
			var genre = await _genreRepository.GetByID(id);
			return genre is null ? null : _mapper.Map<GenreDTO?>(genre);
		}

		public async Task AddGenreAsync(GenreDTO genreDTO)
		{
			if (genreDTO is null)
				throw new ArgumentNullException(nameof(genreDTO));

			var genre = _mapper.Map<Genre>(genreDTO);
			await _genreRepository.Insert(genre);
		}

		public async Task UpdateGenreAsync(GenreDTO genreDTO)
		{
			if (genreDTO is null)
				throw new ArgumentNullException(nameof(genreDTO));

			var genre = _mapper.Map<Genre>(genreDTO);
			await _genreRepository.Update(genre);
		}

		public async Task DeleteGenreAsync(int id)
		{
			await _genreRepository.Delete(id);
		}
	}
}