using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class AuthorRepository : Repo<Author>
    {
        public AuthorRepository(BookStoreContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await context.Authors
                    .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Book).ToListAsync();
        }

        public override Task<Author> GetAsync(long id)
        {
            return context.Authors
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}