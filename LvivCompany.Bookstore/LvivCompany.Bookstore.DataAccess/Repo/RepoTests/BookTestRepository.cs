using System;
using System.Collections.Generic;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.DataAccess.Repo.RepoTests;
using LvivCompany.Bookstore.Entities.Models.ClassTest;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo.RepoTests
{
   public class BookTestRepository : IRepo<BookTest>
    {
        private BookStoreContext context;

        public BookTestRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<BookTest> GetAsync(long id)
        {
            return new BookTest();
        }

        public async Task<IEnumerable<BookTest>> GetAllAsync()
        {
            return await context.BookTests
                    .ToListAsync();
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
                    Amount = 3,
                    Price = 10,
                    Author="Mark Tven",
                    Image="image/c1.jpg"
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
                    Amount = 4,
                    Price = 8,
                    Author="Joanna Rowling",
                     Image="image/c2.jpg"
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
                    Price = 6,
                    Author="Steaven King",
                    Image="image/c3.jpg"
                },

            };

            return books;
        }

        public BookTest Get(long id)
        {
            BookTest b = new BookTest
            {

                SellerName = "Vitalik Dosiak",
                Name = "The Shining",
                Description = "The Shining centers on the life of Jack Torrance, " +
                "an aspiring writer and recovering alcoholic who accepts a position " +
                "as the off-season caretaker of the historic Overlook Hotel in the" +
                " Colorado Rockies. His family accompanies him on this job, including " +
                "his young son Danny Torrance, who possesses the shining, an array of" +
                " psychic abilities that allow Danny to see the hotel's horrific past. " +
                "Soon, after a winter storm leaves them snowbound, the supernatural forces " +
                "inhabiting the hotel influence Jack's sanity, leaving his wife and son in incredible danger.",
                Year = new DateTime(2008, 5, 1, 8, 30, 52),
                Publisher = "FamilyClub",
                NumberOfPages = 320,
                Category = "horror",
                Amount = 9,
                Price = 7,
                Author = "Steaven King",
                Image = "image/c3.jpg"
            };
            return b;
        }

        public void Create(BookTest book)
        {
            context.BookTests.Add(book);
        }

        public async Task CreateAsync(BookTest book)
        {
            await context.BookTests.AddAsync(book);
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
        
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
    
}

