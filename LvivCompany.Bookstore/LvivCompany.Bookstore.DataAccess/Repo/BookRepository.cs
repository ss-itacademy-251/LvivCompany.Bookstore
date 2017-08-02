using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class BookRepository : IRepo<Book>
    {
        private BookStoreContext context;

        public BookRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await context.Books
                    .Include(x => x.Category)
                    .Include(x => x.Publisher)
                    .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author).ToListAsync();
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

        public async Task CreateAsync(Book book)
        {
            await context.Books.AddAsync(book);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
            return await Task.FromResult<Book>(null);
        }
        
        public async Task DeleteAsync(long id)
        {
            Book book = await context.Books.FindAsync(id);
            if (book != null)
               context.Books.Remove(book);  
        }

        public async Task<Book> DeleteAsync(Book book)
        {
            context.Entry(book).State = EntityState.Deleted;
            return await Task.FromResult<Book>(null);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
