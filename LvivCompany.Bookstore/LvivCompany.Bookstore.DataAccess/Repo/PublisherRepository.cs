using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.Repo
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

        public Publisher Get(long id)
        {
            return context.Publishers.Find(id);
        }

        public void Create(Publisher publisher)
        {
            context.Publishers.Add(publisher);
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
    }
}
