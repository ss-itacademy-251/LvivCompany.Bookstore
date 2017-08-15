using LvivCompany.Bookstore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public interface IRepo<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(long id);
        Task CreateAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task DeleteAsync<T>(long id) where T : BaseEntity, new();
        Task DeleteAsync(TEntity item);
        Task SaveAsync();
    }
}