using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class BookRepository : Repo<Book>
    {
        public BookRepository(BookStoreContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await context.Books
                    .Include(x => x.Category)
                    .Include(x => x.Publisher)
                    .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author).ToListAsync();
        }

        public override Task<Book> GetAsync(long id)
        {
            return context.Books
              .Include(x => x.Category)
              .Include(x => x.Publisher)
              .Include(x => x.BookAuthors)
              .ThenInclude(x => x.Author)
              .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBySearch(string text)
        {

                IEnumerable<Book> result = (from books in context.Books where books.Name.Contains(text) select books)
                .Include(x => x.Publisher)
                .Include(x => x.Category)
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author);
                return await Task.FromResult<IEnumerable<Book>>(result);
        }

    }
}