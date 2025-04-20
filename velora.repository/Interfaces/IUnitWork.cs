using velora.core.Data;
using velora.repository.Interfaces;

namespace Store.Repository.Interfaces
{
    public interface IUnitWork
    {
        IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
        Task<int> CompleteAsync();
    }
}