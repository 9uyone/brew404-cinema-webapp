using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class HallService
	{
		private readonly IRepository<Hall> _hallRepository;
		private readonly IMapper _mapper;

		public HallService(IMapper mapper,
			IRepository<Hall> hallRepository)
		{
			_mapper = mapper;
			_hallRepository = hallRepository;
		}

		public async Task<IEnumerable<HallDTO>> GetAllHallsAsync()
		{
			var halls = await Task.Run(() => _hallRepository.Get(orderBy: q => q.OrderBy(h => h.Name)));
			return _mapper.Map<List<HallDTO>>(halls);
		}

		public async Task<HallDTO?> GetHallByIdAsync(int id)
		{
			var hall = await _hallRepository.GetByID(id);
			return hall == null? null :  _mapper.Map<HallDTO?>(hall);
		}

		public async Task AddHallAsync(HallDTO hallDTO)
		{
			var hall = _mapper.Map<Hall>(hallDTO);
			await _hallRepository.Insert(hall);
		}

		public async Task UpdateHallAsync(HallDTO hallDTO)
		{
			var hall = _mapper.Map<Hall>(hallDTO);
			await _hallRepository.Update(hall);
		}

		public async Task DeleteHallAsync(int id)
		{
			await _hallRepository.Delete(id);
		}
	}
}