using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class SearchController : Controller
    {
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookViewModel> _bookmapper;

        public SearchController(IRepo<Book> bookRepo, IMapper<Book, BookViewModel> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            List<Book> books = (await _bookRepo.Get(x => x.Name.Contains(searchText))).ToList();
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(books) });
        }
    }
}
