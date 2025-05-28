namespace KutuphaneDataAcces.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);

        Task AddAsync(TEntity entity);

        IQueryable<TEntity> GetAll();

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<TEntity> GetByIdAsync(int id);

        void RemoveRange(List<TEntity> entities);

        IQueryable<TEntity> Queryable();
    }
}
