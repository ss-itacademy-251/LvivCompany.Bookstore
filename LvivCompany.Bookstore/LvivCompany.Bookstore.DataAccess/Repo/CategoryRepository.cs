using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class CategoryRepository : Repo<Category>
    {
        public CategoryRepository(BookStoreContext context) : base(context)
        {
        }

        public override Task<Category> GetAsync(long id)
        {
            return context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}