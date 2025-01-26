using DataAccess.Context;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

		public virtual IEnumerable<TEntity> Get(
			Expression<Func<TEntity, bool>>? filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = DbSet.AsNoTracking();

			if (filter != null)
				query = query.Where(filter);

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
				return orderBy(query).ToList();
			else
				return query.ToList();
		}

		public virtual async Task<TEntity?> GetByID(int id)
		{
			return await DbSet.AsNoTracking().FirstOrDefaultAsync(el => el.Id == id);
		}

		public async Task Insert(TEntity entity)
		{
			await DbSet.AddAsync(entity);
			await Context.SaveChangesAsync();
		}

		public virtual async Task Update(TEntity entity)
		{
			DbSet.Attach(entity);

			var entry = Context.Entry(entity);
			entry.State = EntityState.Modified;

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
