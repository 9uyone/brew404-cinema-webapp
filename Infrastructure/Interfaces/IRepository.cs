using System.Linq.Expressions;

namespace DataAccess.Interfaces 
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
	{
		public IEnumerable<TEntity> Get(
			Expression<Func<TEntity, bool>>? filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			string includeProperties = "");

		Task<TEntity?> GetByID(int id);
	    Task Insert(TEntity entity);
		Task Update(TEntity entity);
		Task Delete(int id);
    }
}
