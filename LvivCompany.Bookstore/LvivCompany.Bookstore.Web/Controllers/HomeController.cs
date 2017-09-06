using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.Repo;
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
        private IMapper<Book, BookViewModel> _bookmapper;
        private const int countOfBook = 8;
        public HomeController(IRepo<Book> bookRepo, IMapper<Book, BookViewModel> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page )
        {
            if (page==0)
            {
                page = 1;
            }

            List<Book> book = (await _bookRepo.GetPageAsync(x => x.Id > 0, countOfBook, page)).ToList();

            if (book.Count<countOfBook)
            {
                return View(model: new HomePageListViewModel() { Books = _bookmapper.Map(book), PageNumber = page, ExistNext = false});
            }
            return View(model: new HomePageListViewModel() { Books = _bookmapper.Map(book), PageNumber = page, ExistNext = true});
        }

    }
}