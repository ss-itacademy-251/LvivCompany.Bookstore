using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class AuthorRepository : IRepo<Author>
    {
        private BookStoreContext context;

        public AuthorRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await context.Authors
                    .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Book).ToListAsync();
        }

        public async Task<Author> GetAsync(long id)
        {
            return await context.Authors
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Author author)
        {
            await context.Authors.AddAsync(author);
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            context.Entry(author).State = EntityState.Modified;
            return await Task.FromResult<Author>(null);
        }

        public async Task DeleteAsync(long id)
        {
            Author author = await context.Authors.FindAsync(id);
            if (author != null)
                context.Authors.Remove(author);
        }

        public async Task<Author> DeleteAsync(Author author)
        {
            context.Entry(author).State = EntityState.Deleted;
            return await Task.FromResult<Author>(null);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
