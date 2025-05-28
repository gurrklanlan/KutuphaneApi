
using Microsoft.EntityFrameworkCore;

namespace KutuphaneDataAcces.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly KutuphaneDbContext _context;
        private readonly DbSet<TEntity> _dbset;
        
        public GenericRepository(KutuphaneDbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            _dbset.Add(entity);
            _context.SaveChanges();

        }
        
        public async Task AddAsync(TEntity entity)=> await _dbset.AddAsync(entity);
       
        public void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
            _context.SaveChanges(); 
        }

       
        public IQueryable<TEntity> GetAll()=>_dbset.AsQueryable().AsNoTracking();
                  
        public async Task<TEntity> GetByIdAsync(int id)=> await _dbset.FindAsync(id);
       
        public void RemoveRange(List<TEntity> entities)
        {
            _dbset.RemoveRange(entities);
            _context.SaveChanges();
        }
       
        public void Update(TEntity entity)
        {
            _dbset.Update(entity);
            _context.SaveChanges();
        }

        public IQueryable<TEntity> Queryable()=> _dbset.AsQueryable();
       
    }
}
