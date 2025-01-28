using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
	public class HallService
	{
		private readonly IRepository<Hall> _hallRepository;

		public HallService(IRepository<Hall> hallRepository)
		{
			_hallRepository = hallRepository;
		}

		public async Task<IEnumerable<HallDTO>> GetAllHallsAsync()
		{
			var halls = await Task.Run(() => _hallRepository.Get(orderBy: q => q.OrderBy(h => h.Name)));
			return halls.Select(h => new HallDTO
			{
				Id = h.Id,
				Name = h.Name,
				TotalSeats = h.TotalSeats
			});
		}

		public async Task<HallDTO?> GetHallByIdAsync(int id)
		{
			var hall = await _hallRepository.GetByID(id);
			if (hall == null) return null;

			return new HallDTO
			{
				Id = hall.Id,
				Name = hall.Name,
				TotalSeats = hall.TotalSeats
			};
		}

		public async Task AddHallAsync(HallDTO hallDTO)
		{
			var hall = new Hall
			{
				Name = hallDTO.Name,
				TotalSeats = hallDTO.TotalSeats
			};

			await _hallRepository.Insert(hall);
		}

		public async Task UpdateHallAsync(HallDTO hallDTO)
		{
			var hall = new Hall
			{
				Id = hallDTO.Id,
				Name = hallDTO.Name,
				TotalSeats = hallDTO.TotalSeats
			};

			await _hallRepository.Update(hall);
		}

		public async Task DeleteHallAsync(int id)
		{
			await _hallRepository.Delete(id);
		}
	}
}
