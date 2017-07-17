using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public class BookRepository : IRepo <Book>
    {
        private BookStoreContext context;

        public BookRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return context.Books;
        }

        public Book Get(long id)
        {
            return context.Books.Find(id);
        }

        public void Create(Book book)
        {
            context.Books.Add(book);
        }

        public void Update(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Book book = context.Books.Find(id);
            if (book != null)
                context.Books.Remove(book);
        }

        public void Delete(Book book)
        {
            context.Entry(book).State = EntityState.Deleted;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
