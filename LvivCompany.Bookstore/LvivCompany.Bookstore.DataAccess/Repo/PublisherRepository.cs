using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public class PublisherRepository : IRepo<Publisher>
    {
        private BookStoreContext context;

        public PublisherRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await context.Publishers
                    .ToListAsync();
        }

        public async Task<Publisher> GetAsync(long id)
        {
            return await context.Publishers
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Publisher publisher)
        {
            await context.Publishers.AddAsync(publisher);
        }

        public async Task<Publisher> UpdateAsync(Publisher publisher)
        {
            context.Entry(publisher).State = EntityState.Modified;
            return await Task.FromResult<Publisher>(null);
        }

        public async Task DeleteAsync(long id)
        {
            Publisher publisher = await context.Publishers.FindAsync(id);
            if (publisher != null)
                context.Publishers.Remove(publisher);
        }

        public async Task<Publisher> DeleteAsync(Publisher publisher)
        {
            context.Entry(publisher).State = EntityState.Deleted;
            return await Task.FromResult<Publisher>(null);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
