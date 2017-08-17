using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.Repo;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using LvivCompany.Bookstore.Web.ViewModels;
using LvivCompany.Bookstore.Web.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookViewModel> _bookmapper;

        public HomeController(IRepo<Book> bookRepo, IMapper<Book, BookViewModel> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
            
        }


        public async Task<IActionResult> Index()
        {
            var book = (await _bookRepo.GetAllAsync()).ToList();
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(book)});
        }
        [HttpPost]
        public IActionResult Index(long id)
        {

            return RedirectToAction("Index", "BookDetail", id);
        }
    }
}