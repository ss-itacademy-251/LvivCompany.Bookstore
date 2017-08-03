using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class BookDetailMapper : IMapp<Book, BookDetailViewModel>
    {


        public BookDetailMapper()
        {
            
        }

        public BookDetailViewModel Map(Book entity)
        {
            BookDetailViewModel tempModel = new BookDetailViewModel();
            tempModel.Name = entity.Name;
            tempModel.Price = entity.Price;
            tempModel.NumberOfPages = entity.NumberOfPages;
            tempModel.Year = entity.Year;
            tempModel.Publisher = entity.Publisher.Name;
            tempModel.Amount = entity.Amount;
            tempModel.Category = entity.Category.Name;
            tempModel.Description = entity.Description;
            tempModel.Authors= entity.BookAuthors.Select(a => a.Author).ToList();
            return tempModel;
        }
        public Book Map(BookDetailViewModel model)
        {
            Book tempBook = new Book();
            
            return tempBook;
        }
        public Book Map(BookDetailViewModel model, Book tempBook)
        {
            
            return tempBook;
        }
        public List<BookDetailViewModel> Map(List<Book> entity)
        { List<BookDetailViewModel> models = new List<BookDetailViewModel>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }
            return models;
        }
        public List<Book> Map(List<BookDetailViewModel> models)
        {
            List<Book> books = new List<Book>();
            foreach (var item in models)
            {
                books.Add(Map(item));
            }
            return books;
        }
    }
}
