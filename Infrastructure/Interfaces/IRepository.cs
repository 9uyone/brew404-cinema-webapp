using System.Linq.Expressions;

namespace DataAccess.Interfaces 
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
	{
		public Task<IEnumerable<TEntity>> Get(
			Expression<Func<TEntity, bool>>? filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			string includeProperties = "", bool tracking = false);

		Task<TEntity?> GetByID(int id);
		Task<TEntity?> GetByID(int id, string includeProperties = "");
		Task Insert(TEntity entity);
		Task AddRange(ICollection<TEntity> entities);
		Task Update(TEntity entity);
		Task Update(TEntity entity, List<string> propertiesToUpdate);
		Task Delete(int id);
    }
}