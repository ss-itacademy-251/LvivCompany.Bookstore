using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public interface IRepo<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(long id);
        Task CreateAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task DeleteAsync(long id);
        Task<TEntity> DeleteAsync(TEntity item);
        Task SaveAsync();
    }
}
