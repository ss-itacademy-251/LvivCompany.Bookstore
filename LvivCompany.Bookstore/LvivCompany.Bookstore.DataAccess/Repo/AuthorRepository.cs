using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public class AuthorRepository : IRepo<Author>
    {
        private BookStoreContext context;

        public AuthorRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Author> GetAll()
        {
            return context.Authors;
        }

        public Author Get(long id)
        {
            return context.Authors.Find(id);
        }

        public void Create(Author author)
        {
            context.Authors.Add(author);
        }

        public void Update(Author author)
        {
            context.Entry(author).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Author author = context.Authors.Find(id);
            if (author != null)
                context.Authors.Remove(author);
        }

        public void Delete(Author author)
        {
            context.Entry(author).State = EntityState.Deleted;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
