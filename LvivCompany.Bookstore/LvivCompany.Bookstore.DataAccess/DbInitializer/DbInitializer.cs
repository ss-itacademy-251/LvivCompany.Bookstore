﻿using LvivCompany.Bookstore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LvivCompany.Bookstore.DataAccess
{
    public static class DbInitializer
    {
        public static void Seed(BookStoreContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            List<Category> categories = new List<Category>
            {
                new Category {AddedDate=DateTime.UtcNow, Name="Other" },
                new Category {AddedDate=DateTime.UtcNow, Name="Business"},
                new Category {AddedDate=DateTime.UtcNow, Name="Kids"},
                new Category {AddedDate=DateTime.UtcNow, Name="Comics"},
                new Category {AddedDate=DateTime.UtcNow, Name="Cooking"},
                new Category {AddedDate=DateTime.UtcNow, Name="History"},
                new Category {AddedDate=DateTime.UtcNow, Name="Fantasy"},
                new Category {AddedDate=DateTime.UtcNow, Name="Science"},
                new Category {AddedDate=DateTime.UtcNow, Name="Fiction"}
            };

            foreach (var item in categories)
                context.Categories.Add(item);

            context.SaveChanges();
        }
    }
}