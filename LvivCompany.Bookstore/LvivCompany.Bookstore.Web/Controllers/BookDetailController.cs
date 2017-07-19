using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities.Models.ClassTest;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookDetailController : Controller
    {
        private IRepo<BookTest> _bookRepo;

        public BookDetailController(IRepo<BookTest> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public IActionResult Index()
        {
            var book = _bookRepo.Get(1);

            return View(book);
          
        }
    }
}