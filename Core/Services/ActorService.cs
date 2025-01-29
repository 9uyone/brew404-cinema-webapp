using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
	public class ActorService
	{
		private IMapper _mapper;
		private IRepository<Actor> _actorRepository;

		public ActorService(IMapper mapper,
			IRepository<Actor> repository)
		{
			_mapper = mapper;
			_actorRepository = repository;
		}

		public async Task<IEnumerable<ActorDTO>> GetAllActorsAsync()
		{
			var actors = await Task.Run(() => _actorRepository.Get(orderBy: q => q.OrderBy(g => g.Name)));
			return _mapper.Map<List<ActorDTO>>(actors);
		}

		public async Task<ActorDTO?> GetActorByIdAsync(int id)
		{
			var actor = await _actorRepository.GetByID(id);
			return actor == null ? null : _mapper.Map<ActorDTO?>(actor);
		}

		public async Task AddActorAsync(ActorDTO actorDTO)
		{
			var actor = _mapper.Map<Actor>(actorDTO);
			await _actorRepository.Insert(actor);
		}

		public async Task UpdateActorAsync(ActorDTO actorDTO)
		{
			var actor = _mapper.Map<Actor>(actorDTO);
			await _actorRepository.Update(actor);
		}

		public async Task DeleteActorAsync(int id)
		{
			await _actorRepository.Delete(id);
		}
	}
}
