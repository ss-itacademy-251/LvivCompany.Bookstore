using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
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

        public Status Get(long id)
        {
            return context.Statuses.Find(id);
        }

        public void Create(Status status)
        {
            context.Statuses.Add(status);
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
    }
}
