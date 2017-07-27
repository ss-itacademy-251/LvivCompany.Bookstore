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

        public IEnumerable<Publisher> GetAll()
        {
            return context.Publishers;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await context.Publishers
                    .ToListAsync();
        }

        public Publisher Get(long id)
        {
            return context.Publishers.Find(id);
        }

        public async Task<Publisher> GetAsync(long id)
        {
            return await context.Publishers
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Create(Publisher publisher)
        {
            context.Publishers.Add(publisher);
        }

        public async Task CreateAsync(Publisher publisher)
        {
            await context.Publishers.AddAsync(publisher);
        }

        public void Update(Publisher publisher)
        {
            context.Entry(publisher).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Publisher publisher = context.Publishers.Find(id);
            if (publisher != null)
                context.Publishers.Remove(publisher);
        }

        public void Delete(Publisher publisher)
        {
            context.Entry(publisher).State = EntityState.Deleted;
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
