using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using velora.core.Data;
using velora.core.Entites;
using velora.repository.Specifications;

namespace velora.repository.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity> Spec);
        Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity> Spec);
        Task AddAsync(TEntity entity);
        Task<int> CountAsync(ISpecifications<TEntity> spec);
    }
}
