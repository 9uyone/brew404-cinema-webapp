using DataAccess.Context;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

		public virtual async Task<IEnumerable<TEntity>> Get(
			Expression<Func<TEntity, bool>>? filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			string includeProperties = "",
			bool tracking = false)
		{
			IQueryable<TEntity> query = DbSet;

			if (!tracking)
				query = query.AsNoTracking();

			if (filter != null)
				query = query.Where(filter);

			foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
				return await orderBy(query).ToListAsync();
			else
				return await query.ToListAsync();
		}


		public async Task<TEntity?> GetByID(int id, string includeProperties = "")
		{
			IQueryable<TEntity> query = DbSet.AsNoTracking();

			foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			return await query.FirstOrDefaultAsync(el => el.Id == id);
		}

		public virtual async Task<TEntity?> GetByID(int id)
		{
			return await DbSet.FirstOrDefaultAsync(el => el.Id == id);
		}

		public async Task Insert(TEntity entity)
		{
			await DbSet.AddAsync(entity);
			await Context.SaveChangesAsync();
		}

		public async Task AddRange(ICollection<TEntity> entities)
		{
			await DbSet.AddRangeAsync(entities);
			await Context.SaveChangesAsync();
		}

		public virtual async Task Update(TEntity entity)
		{
			var entry = Context.Entry(entity);

			if (entry.State == EntityState.Detached)
				DbSet.Attach(entity);

			foreach(var property in entry.Properties)
			{
				if (property.Metadata.Name != nameof(entity.Id))
					property.IsModified = true;
			}

			await Context.SaveChangesAsync();
		}

		public async Task Update(TEntity entity, List<string> propertiesToUpdate)
		{
			var entry = Context.Entry(entity);

			if (entry.State == EntityState.Detached)
				DbSet.Attach(entity);

			foreach(var property in entry.Properties)
			{
				if (propertiesToUpdate.Contains(property.Metadata.Name)
					&& property.CurrentValue != null)
					property.IsModified = true;
			}

			await Context.SaveChangesAsync();
		}

		public virtual async Task Delete(int id)
		{
			await DbSet
				.Where(el => el.Id == id)
				.ExecuteDeleteAsync();

		}

		private bool _dispose = false;

		public virtual void Dispose()
		{
			if(!_dispose)
			{
				Context?.Dispose();
				_dispose = true;
			}
		}

	}
}
