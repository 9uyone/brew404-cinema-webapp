namespace DataAccess.Interfaces 
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
	{
		Task<IEnumerable<TEntity>> GetAll();
		Task<TEntity?> Get(Guid id);
	    Task Insert(TEntity entity);
		Task Update(TEntity entity);
		Task Delete(Guid id);
    }
}
