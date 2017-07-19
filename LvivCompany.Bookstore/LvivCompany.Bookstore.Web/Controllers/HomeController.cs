using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.IRepo;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepo<Book> _bookRepo;

        public HomeController(IRepo<Book> bookRepo)
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