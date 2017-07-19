using System;
using System.Collections.Generic;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.DataAccess.Repo.RepoTests
{
    class CategoryTestRepository: IRepo<Category>
    {
        private BookStoreContext context;

        public CategoryTestRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            var category = new List<Category>
            {
                new Category
                {
                    Id=1,
                    Name = "history"
                },
                 new Category
                {
                    Id=2,
                    Name = "thriller"
                },
                  new Category
                {
                    Id=3,
                    Name = "horror"
                },
                   new Category

                {
                    Id=4,
                    Name = "detective"
                },
                    new Category
                {
                    Id=5,
                    Name = "comedy"
                },
                     new Category
                {
                    Id=6,
                    Name = "drama"
                }
            };
            return category;
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
