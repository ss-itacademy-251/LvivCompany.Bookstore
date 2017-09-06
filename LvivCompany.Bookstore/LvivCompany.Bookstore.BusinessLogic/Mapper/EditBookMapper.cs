using System.Collections.Generic;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;

namespace LvivCompany.Bookstore.BusinessLogic.Mapper
{
    public class EditBookMapper : IMapper<Book, EditBookViewModel>
    {
        public EditBookMapper()
        {
        }

        public EditBookViewModel Map(Book book)
        {
            EditBookViewModel model = new EditBookViewModel
            {
                Id = book.Id,
                Amount = book.Amount,
                CategoryId = book.CategoryId,
                Category = book.Category.Name,
                NumberOfPages = book.NumberOfPages,
                Description = book.Description,
                Name = book.Name,
                Price = book.Price,
                Year = book.Year,
                ImageUrl = book.ImageUrl,
            };

            return model;
        }

        public Book Map(EditBookViewModel model)
        {
            return new Book();
        }

        public Book Map(EditBookViewModel model, Book book)
        {
            book.Name = model.Name;
            book.Year = model.Year;
            book.NumberOfPages = model.NumberOfPages;
            book.Amount = model.Amount;
            book.Description = model.Description;
            book.Price = model.Price;
            book.CategoryId = model.CategoryId;
            return book;
        }

        public List<EditBookViewModel> Map(List<Book> entity)
        {
            List<EditBookViewModel> models = new List<EditBookViewModel>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }

            return models;
        }

        public List<Book> Map(List<EditBookViewModel> model)
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