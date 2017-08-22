using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public abstract class Repo<TEntity> : IRepo<TEntity> where TEntity : BaseEntity
    {
        protected BookStoreContext context;
        
        public Repo(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual Task<TEntity> GetAsync(long id)
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public virtual Task CreateAsync(TEntity item)
        {
            context.Set<TEntity>().Add(item);
            return SaveAsync();
        }
        public virtual Task UpdateAsync(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            return SaveAsync();
        }

        public virtual Task DeleteAsync<T>(long id) where T : BaseEntity, new()
        {
            var entity = context.Set<T>().Find(id);
            if (entity != null)
                context.Set<T>().Remove(entity);
            return SaveAsync();
        }

        public virtual Task DeleteAsync(TEntity item)
        {
            context.Entry(item).State = EntityState.Deleted;
            return SaveAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter)
         {
             IQueryable<TEntity> query = context.Set<TEntity>();
             query = query.Where(filter);
             return await query.ToListAsync();
         }
}
}