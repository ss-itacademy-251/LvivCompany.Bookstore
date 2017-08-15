using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;
using System;
using System.Collections.Generic;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class BookMapper : IMapper<Book, BookViewModel>
    {
        public BookMapper()
        {
        }

        public BookViewModel Map(Book book)
        {

            BookViewModel model = new BookViewModel
            {
                Id = book.Id,
                Amount = book.Amount,
                CategoryId = book.CategoryId,
                Category = book.Category.Name,
                PublisherName = book.Publisher.Name,
                NumberOfPages = book.NumberOfPages,
                Description = book.Description,
                Name = book.Name,
                Price = book.Price,
                Year = book.Year,
                ImageUrl = book.ImageUrl,
                Authors = new List<AuthorViewModel>()
            };

            foreach (var bookAuthor in book.BookAuthors)
            {
                model.Authors.Add(new AuthorViewModel
                {
                    FirstName = bookAuthor.Author.FirstName,
                    LastName = bookAuthor.Author.LastName
                });
            }

            return model;
        }

        public Book Map(BookViewModel model)
        {
          
            Publisher publisher = new Publisher()
            {
                AddedDate = DateTime.UtcNow,
                Name = model.PublisherName
            };

            Book book = new Book()
            {
                AddedDate = DateTime.UtcNow,
                Name = model.Name,
                Year = model.Year,
                NumberOfPages = model.NumberOfPages,
                Amount = model.Amount,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Publisher = publisher,
                BookAuthors = new List<BookAuthor>()
            };

            foreach (var author in model.Authors)
            {
                BookAuthor bookAuthor = new BookAuthor
                {
                    Book = book,
                    Author = new Author
                    {
                        AddedDate = DateTime.UtcNow,
                        FirstName = author.FirstName,
                        LastName = author.LastName
                    }
                };
                book.BookAuthors.Add(bookAuthor);
            }

            return book;
        }

        public Book Map(BookViewModel model, Book tempBook)
        {
            tempBook.Name = model.Name;
            tempBook.Price = model.Price;
            return tempBook;
        }

        public List<BookViewModel> Map(List<Book> entity)
        {
            List<BookViewModel> models = new List<BookViewModel>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }
            return models;
        }

        public List<Book> Map(List<BookViewModel> model)
        {
            List<Book> books = new List<Book>();
            foreach (var item in model)
            {
                books.Add(Map(item));
            }
            return books;
        }
    }
}