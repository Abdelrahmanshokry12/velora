using Microsoft.EntityFrameworkCore;
using velora.core.Data;
using velora.repository.Interfaces;
using velora.repository.Specifications;
using velora.core.Data.Contexts;



namespace velora.repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext _dbContext;
        public GenericRepository(StoreContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();

        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }
        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity> Spec)
        {
            return SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), Spec);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }
        public async Task<int> CountAsync(ISpecifications<TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
    }
}

