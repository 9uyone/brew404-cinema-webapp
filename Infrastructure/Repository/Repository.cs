using DataAccess.Context;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
	{
		public CinemaDbContext Context { get; }
		public DbSet<TEntity> DbSet { get; }

		public Repository(CinemaDbContext context)
		{
			Context = context;
			DbSet = Context.Set<TEntity>();
		}

		public async Task<IEnumerable<TEntity>> GetAll()
		{
			return await DbSet.AsNoTracking().ToListAsync();
		}

		public async Task<TEntity?> Get(Guid id)
		{
			return await DbSet.AsNoTracking().FirstOrDefaultAsync(el => el.Id == id);
		}

		public async Task Insert(TEntity entity)
		{
			await DbSet.AddAsync(entity);
			await Context.SaveChangesAsync();
		}

		/// <summary>
		/// in progress...
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task Update(TEntity entity)
		{
			await DbSet
				.Where(el => el.Id == entity.Id)
				.ExecuteUpdateAsync(el => el
				//	.SetProperty()
					);
				
			throw new NotImplementedException();
		}

		public async Task Delete(Guid id)
		{
			await DbSet
				.Where(el => el.Id == id)
				.ExecuteDeleteAsync();

		}

		private bool _dispose = false;
		public void Dispose()
		{
			if(!_dispose)
			{
				Context.Dispose();
				_dispose = true;
			}
		}

	}
}
