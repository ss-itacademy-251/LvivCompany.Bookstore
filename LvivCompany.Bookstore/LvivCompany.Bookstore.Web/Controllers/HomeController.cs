using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities.Models.ClassTest;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepo<BookTest> _bookRepo;

        public HomeController(IRepo<BookTest> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public IActionResult Index()
        {
            var books = _bookRepo.GetAll();

            return View(books);
        }
    }
}