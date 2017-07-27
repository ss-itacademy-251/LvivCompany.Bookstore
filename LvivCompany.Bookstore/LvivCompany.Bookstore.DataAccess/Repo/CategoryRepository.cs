using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class CategoryRepository : IRepo<Category>
    {
        private BookStoreContext context;

        public CategoryRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories;
        }

        public Category Get(long id)
        {
            return context.Categories.Find(id);
        }

        public void Create(Category category)
        {
            context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Category category = context.Categories.Find(id);
            if (category != null)
                context.Categories.Remove(category);
        }

        public void Delete(Category category)
        {
            context.Entry(category).State = EntityState.Deleted;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
