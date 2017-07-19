using System;
using System.Collections.Generic;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.DataAccess.Repo.RepoTests;
using LvivCompany.Bookstore.Entities.Models.ClassTest;

namespace LvivCompany.Bookstore.DataAccess.Repo.RepoTests
{
    public class BookTestRepository : IRepo<BookTest>
    {
        private BookStoreContext context;

        public BookTestRepository(BookStoreContext context)
        {
            this.context = context;
        }
        

        public IEnumerable<BookTest> GetAll()
        {
            var category = new List<Category>
            {
              new Category
              {
                 Id=1,
                 Name="history"
              },
              new Category
              {
                 Id=2,
                 Name="thriller"
              },
              new Category
              {
                 Id=3,
                 Name="horror"
              },
              new Category
              {
                 Id=4,
                 Name="detective"
              },
              new Category
              {
                 Id=5,
                 Name="comedy"
              },
              new Category
              {
                 Id=6,
                 Name="drama"
              },
               new Category
              {
                 Id=7,
                 Name="adventure"
              },
                new Category
              {
                 Id=8,
                 Name="fantasy"
              },
            };
         

            var books = new List<BookTest>
            {
                new BookTest
                {
                    
                    SellerName = "Anna Avramenko",
                    Name = "Tom Sawyer",
                    Description = "story about adventure",
                    Year = new DateTime(2008, 5, 1, 8, 30, 52),
                    Publisher = "Ranok",
                    NumberOfPages = 380,
                    Category =category[6].Name,
                    Amount = 9,
                    Price = 10,
                    Author="Mark Tven"
                },
                 new BookTest
                {

                    SellerName = "Mark Spilnyk",
                    Name = "Harry Potter",
                    Description = "story about boy who alive",
                    Year = new DateTime(2008, 5, 1, 8, 30, 52),
                    Publisher = "Ababagalamaga",
                    NumberOfPages = 290,
                    Category =category[7].Name,
                    Amount = 9,
                    Price = 10,
                    Author="Joanna Rowling"
                },

                  new BookTest
                {

                    SellerName = "Vitalik Dosiak",
                    Name = "The Shining",
                    Description = "story about adventure",
                    Year = new DateTime(2008, 5, 1, 8, 30, 52),
                    Publisher = "FamilyClub",
                    NumberOfPages = 320,
                    Category =category[0].Name,
                    Amount = 9,
                    Price = 10,
                    Author="Steaven King"
                },

            };

            return books;
        }

        public BookTest Get(long id)
        {
            return context.BookTests.Find(id);
        }

        public void Create(BookTest book)
        {
            context.BookTests.Add(book);
        }

        public void Update(BookTest book)
        {
            context.Entry(book).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            BookTest book = context.BookTests.Find(id);
            if (book != null)
                context.BookTests.Remove(book);
        }

        public void Delete(BookTest book)
        {
            context.Entry(book).State = EntityState.Deleted;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}

