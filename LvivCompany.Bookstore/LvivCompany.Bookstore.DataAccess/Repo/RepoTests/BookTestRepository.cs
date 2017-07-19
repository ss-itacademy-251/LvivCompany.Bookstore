using System;
using System.Collections.Generic;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.DataAccess.Repo.RepoTests
{
    public class BookTestRepository : IRepo<Book>
    {
        private BookStoreContext context;

        public BookTestRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Book> GetAll()
        {
           // Category cat = new Category("history");
           var books = new List<Book>
            {
                new Book
                {

                    SellerId = 1,
                    Name = "Harry Potter",
                    Description = "dfbgnermgftthdsgs",
                    Year = new DateTime(2008, 5, 1, 8, 30, 52),
                    PublisherId = 1,
                    NumberOfPages = 280,
                   // CategoryId =cat ,
                    Amount = 9,
                    Price = 10
                },
                  new Book
                {
                    SellerId = 1,
                    Name = "LoR",
                    Description = "fklky;",
                    Year = new DateTime(1998, 5, 1, 8, 30, 52),
                    PublisherId = 1,
                    NumberOfPages = 980,
                    CategoryId = 1,
                    Amount = 7,
                    Price = 15
                },

                  new Book
                {
                    SellerId = 1,
                    Name = "Narnia",
                    Description = "skuhdftdhkr",
                    Year =  new DateTime(1978, 5, 1, 8, 30, 52),
                    PublisherId = 1,
                    NumberOfPages = 268,
                    CategoryId = 1,
                    Amount = 7,
                    Price = 20
                }

            };

            return books;
        }

        public Book Get(long id)
        {
            return context.Books.Find(id);
        }

        public void Create(Book book)
        {
            context.Books.Add(book);
        }

        public void Update(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Book book = context.Books.Find(id);
            if (book != null)
                context.Books.Remove(book);
        }

        public void Delete(Book book)
        {
            context.Entry(book).State = EntityState.Deleted;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}

