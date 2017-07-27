using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Entities.Models.ClassTest;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public interface IRepo<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(long id);
        Task<TEntity> GetAsync(long id);
        void Create(TEntity item);
        //Task CreateAsync(TEntity item);
        void Update(TEntity item);
        //Task UpdateAsync(TEntity item);
        void Delete(long id);
        //Task DeleteAsync(long id);
        void Delete(TEntity item);
        //Task DeleteAsync(TEntity item);

    }
}
