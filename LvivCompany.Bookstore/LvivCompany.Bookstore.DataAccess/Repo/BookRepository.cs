using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public class BookRepository : IRepo<Book>
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

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await context.Books
                    .Include(x => x.Category)
                    .Include(x => x.Publisher)
                    .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author).ToListAsync();
        }

        public Book Get(long id)
        {
            return context.Books.Find(id);
        }

        public async Task<Book> GetAsync(long id)
        {
                  return await context.Books
                    .Include(x => x.Category)
                    .Include(x => x.Publisher)
                    .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Create(Book book)
        {
            context.Books.Add(book);
        }

        public async Task CreateAsync(Book book)
        {
            await context.Books.AddAsync(book);
        }

        public void Update(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
        }

        public async Task UpdateAsync(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Book book = context.Books.Find(id);
            if (book != null)
                context.Books.Remove(book);
        }
        
        public async Task DeleteAsync(long id)
        {
            Book book = await context.Books.FindAsync(id);
            if (book != null)
                context.Books.Remove(book);
        }

        public void Delete(Book book)
        {
            context.Entry(book).State = EntityState.Deleted;
        }

        public async Task DeleteAsync(Book book)
        {
            context.Entry(book).State = EntityState.Deleted;
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
