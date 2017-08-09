using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.DataAccess;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Web.ViewModels;
using System.Linq;
using System.Collections.Generic;
using LvivCompany.Bookstore.Web.Mapper;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepo<Book> _bookRepo;
        private IMapp<Book, BookInfo> _bookmapper;

        public HomeController(IRepo<Book> bookRepo, IMapp<Book, BookInfo> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Book> book = (await _bookRepo.GetAllAsync()).ToList();
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(book) });
        }
        [HttpPost]
        public async Task<IActionResult> Index(long id)
        {


            return RedirectToAction("Index", "BookDetail", id);
        }
    }
}