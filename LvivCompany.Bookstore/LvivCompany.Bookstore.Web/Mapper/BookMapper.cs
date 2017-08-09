using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class BookMapper : IMapper<Book, BookInfo>
    {


        public BookMapper()
        {
            
        }

        public BookInfo Map(Book entity)
        {
            BookInfo tempBookInfo = new BookInfo();
            tempBookInfo.Name = entity.Name;
            tempBookInfo.Price = entity.Price;
            tempBookInfo.Id = entity.Id;
            return tempBookInfo;
        }
        public Book Map(BookInfo model)
        {
            Book tempBook = new Book();
            tempBook.Name = model.Name;
            tempBook.Price = model.Price;
            tempBook.Id = model.Id;
            return tempBook;
        }
        public Book Map(BookInfo model,Book tempBook)
        {
            tempBook.Name = model.Name;
            tempBook.Price = model.Price;
            return tempBook;
        }

        public List<BookInfo> Map(List<Book> entity)
        {
            
            List<BookInfo> models= new List<BookInfo>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }
            return models;
           
        }
        public List<Book> Map(List<BookInfo> model)
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
