using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.IRepo;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using LvivCompany.Bookstore.Web.Config;
using LvivCompany.Bookstore.Web.ViewModels;
using LvivCompany.Bookstore.Web.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private AppConfiguration Configuration;
       
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookInfo> _bookmapper;

        public HomeController(IRepo<Book> bookRepo, IMapper<Book, BookInfo> bookmapper, IOptionsSnapshot<AppConfiguration> configuration)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
            Configuration = configuration.Value;
        }


        public async Task<IActionResult> Index()
        {
            var book = (await _bookRepo.GetAllAsync()).ToList();
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(book)});
        }
    }
}