using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    /// <summary>
    /// Represents StatusRepository class
    /// </summary>
    /// <seealso cref="LvivCompany.Bookstore.DataAccess.IRepo.IRepo{LvivCompany.Bookstore.Entities.Status}" />
    public class StatusRepository : IRepo<Status>
    {
        private BookStoreContext context;

        public StatusRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Status> GetAll()
        {
            return context.Statuses;
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await context.Statuses
                    .ToListAsync();
        }

        public Status Get(long id)
        {
            return context.Statuses.Find(id);
        }

        public async Task<Status> GetAsync(long id)
        {
            return await context.Statuses
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public void Create(Status status)
        {
            context.Statuses.Add(status);
        }

        public async Task CreateAsync(Status status)
        {
            await context.Statuses.AddAsync(status);
        }

        public void Update(Status status)
        {
            context.Entry(status).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Status status = context.Statuses.Find(id);
            if (status != null)
                context.Statuses.Remove(status);
        }

        public void Delete(Status status)
        {
            context.Entry(status).State = EntityState.Deleted;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
