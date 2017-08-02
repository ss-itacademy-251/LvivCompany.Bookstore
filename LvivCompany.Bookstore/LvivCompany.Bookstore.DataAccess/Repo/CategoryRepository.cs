using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class CategoryRepository : IRepo<Category>
    {
        private BookStoreContext context;

        public CategoryRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await context.Categories
                  .ToListAsync();
        }

        public async Task<Category> GetAsync(long id)
        {
            return await context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Category category)
        {
            await context.Categories.AddAsync(category);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            return await Task.FromResult<Category>(null);
        }

        public async Task DeleteAsync(long id)
        {
            Category category = await context.Categories.FindAsync(id);
            if (category != null)
                context.Categories.Remove(category);
        }

        public async Task<Category> DeleteAsync(Category category)
        {
            context.Entry(category).State = EntityState.Deleted;
            return await Task.FromResult<Category>(null);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
