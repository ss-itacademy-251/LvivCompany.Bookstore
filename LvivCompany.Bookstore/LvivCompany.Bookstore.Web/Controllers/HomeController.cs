using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.DataAccess;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Web.ViewModels;
using System.Linq;
using System.Collections.Generic;
using LvivCompany.Bookstore.Web.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookViewModel> _bookmapper;
        private UserManager<User> _userManager;

        public HomeController(IRepo<Book> bookRepo, IMapper<Book, BookViewModel> bookmapper ,UserManager<User> userManager)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Book> book = (await _bookRepo.GetAllAsync()).ToList();
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(book) });
        }
        [Authorize(Roles = "Seller")]
        [HttpGet]
        public async Task<IActionResult> SellersBook()
        {
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            List<Book> book = (await (_bookRepo.Get(x=>x.SellerId==currentUserId))).ToList();
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(book) });
        }
        [HttpPost]
        public IActionResult Index(long id)
        {

    }
}