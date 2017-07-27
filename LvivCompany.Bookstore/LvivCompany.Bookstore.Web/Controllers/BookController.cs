using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Entities.Models;
using LvivCompany.Bookstore.DataAccess.Repo;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookController : Controller
    {
        private IRepo<Book> repoBook;
        private IRepo<Author> repoAuthor;
        private IRepo<Publisher> repoPublisher;
        private IRepo<Category> repoCategory;

        public BookController(IRepo<Author> repoAuthor, IRepo<Book> repoBook, IRepo<Publisher> repoPublisher, IRepo<Category> repoCategory)
        {
            this.repoAuthor = repoAuthor;
            this.repoBook = repoBook;
            this.repoPublisher = repoPublisher;
            this.repoCategory = repoCategory;
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            BookViewModel model = new BookViewModel
            {
                Categories = repoCategory.GetAll().Select(c => new SelectListItem
                {
                    Text = $"{c.Name}",
                    Value = c.Id.ToString()
                }).ToList()
            };
            return View("AddBook", model);
        }

        [HttpPost]
        public IActionResult AddBook(BookViewModel model)
        {
            foreach (var item in model.Authors)
            {
                item.AddedDate = DateTime.UtcNow;
                repoAuthor.Create(item);
            }
            repoAuthor.Save();
            Publisher publisher = new Publisher()
            {
                Name = model.PublisherName
            };
            repoPublisher.Create(publisher);
            repoPublisher.Save();

            Book book = new Book()
            {
                AddedDate = DateTime.UtcNow,
                Name = model.Name,
                Year = model.Year,
                NumberOfPages = model.NumberOfPages,
                Amount = model.Amount,
                Description = model.Description,
                Price = model.Price
            };
            using (var memoryStream = new MemoryStream())
            {
                model.Image.CopyTo(memoryStream);
                book.Image = memoryStream.ToArray();
            }
            repoBook.Create(book);
            return RedirectToAction("Index", "Home");
        }
    }
}
