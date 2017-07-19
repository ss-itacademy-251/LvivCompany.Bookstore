using System;
using System.Collections.Generic;
using System.Text;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Entities.Models.ClassTest;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public interface IRepo<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(long id);
        void Delete(TEntity item);

    }
}
