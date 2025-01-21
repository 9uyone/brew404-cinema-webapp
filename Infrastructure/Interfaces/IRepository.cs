namespace DataAccess.Interfaces {
    public interface IRepository<TEntity> : IEntity
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
        void Delete(TEntity entity);
        
        void Save();
    }
}
