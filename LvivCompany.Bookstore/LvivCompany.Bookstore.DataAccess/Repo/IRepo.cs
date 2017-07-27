using System;
using System.Collections.Generic;
using System.Text;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public interface IRepo<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(long id);
        void Delete(TEntity item);
        void Save();
    }
}
