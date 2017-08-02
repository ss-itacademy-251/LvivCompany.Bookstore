using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
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

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await context.Statuses
                    .ToListAsync();
        }

        public async Task<Status> GetAsync(long id)
        {
            return await context.Statuses
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task CreateAsync(Status status)
        {
            await context.Statuses.AddAsync(status);
        }

        public async Task<Status> UpdateAsync(Status status)
        {
            context.Entry(status).State = EntityState.Modified;
            return await Task.FromResult<Status>(null);
        }

        public async Task DeleteAsync(long id)
        {
            Status status = await context.Statuses.FindAsync(id);
            if (status != null)
                context.Statuses.Remove(status);
        }

        public async Task<Status> DeleteAsync(Status status)
        {
            context.Entry(status).State = EntityState.Deleted;
            return await Task.FromResult<Status>(null);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
