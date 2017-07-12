using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public class BookRepository : IRepo<Book>
    {
        private BookstoreContext db;

        public BookRepository()
        {
            this.db = new BookstoreContext();
        }

        public IEnumerable<Book> GetBookList()
        {
            return db.Books;
        }

        public Book Get(long id)
        {
            return db.Books.Find(id);
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Update(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
