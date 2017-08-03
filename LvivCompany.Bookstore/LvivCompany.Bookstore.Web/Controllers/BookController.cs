﻿using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookController : Controller
    {
        private IRepo<Book> repoBook;
        private IRepo<Author> repoAuthor;
        private IRepo<Category> repoCategory;

        public BookController(IRepo<Author> repoAuthor, IRepo<Book> repoBook, IRepo<Category> repoCategory)
        {
            this.repoAuthor = repoAuthor;
            this.repoBook = repoBook;
            this.repoCategory = repoCategory;
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            BookViewModel model = new BookViewModel();
            await PopulateCategoriesSelectList(model);
            return View("AddBook", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                await SaveBookToDb(model);
                return RedirectToAction("Index", "Home");
            }
            await PopulateCategoriesSelectList(model);
            return View(model);
        }

        private async Task PopulateCategoriesSelectList(BookViewModel model)
        {
            var categoryList = await repoCategory.GetAllAsync();
            model.Categories = categoryList.Select(c => new SelectListItem
            {
                Text = $"{c.Name}",
                Value = c.Id.ToString()
            }).ToList();
        }

        public async Task SaveBookToDb(BookViewModel model)
        {
            List<Author> Authors = new List<Author>();
            for (int i = 0; i < model.Authors.Count; i++)
            {
                Authors.Add(new Author
                {
                    AddedDate = DateTime.UtcNow,
                    FirstName = model.Authors[i].FirstName,
                    LastName = model.Authors[i].LastName
                });
                await repoAuthor.CreateAsync(Authors[i]);
            }

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
                CategoryId = (await repoCategory.GetAllAsync()).SingleOrDefault(c => c.Id == model.CategoryId).Id,
                Publisher = publisher,
                BookAuthors = new List<BookAuthor>()
            };
            if (model.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.Image.CopyTo(memoryStream);
                    book.Image = memoryStream.ToArray();
                }
            }
            for (int i = 0; i < model.Authors.Count; i++)
            {
                BookAuthor bookAuthor = new BookAuthor { BookId = book.Id, AuthorId = Authors[i].Id };
                book.BookAuthors.Add(bookAuthor);
            }
            await repoBook.CreateAsync(book);
            await repoBook.SaveAsync();
        }
    }
}