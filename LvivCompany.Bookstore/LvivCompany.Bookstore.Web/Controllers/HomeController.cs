using System.Threading.Tasks;
using LvivCompany.Bookstore.BusinessLogic;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private const int CountOfBook = 8;
        private HomeServices services;
        private UserManager<User> _userManager;

        public HomeController(HomeServices services, UserManager<User> userManager)
        {
            this.services = services;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page)
        {
            return View(await services.GetBooksPageAsync(page, CountOfBook, x => x.Id > 0));
        }

        [Authorize(Roles = "Seller")]
        [HttpGet]
        public async Task<IActionResult> SellersBook(int page)
        {
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            return View(await services.GetBooksPageAsync(page, CountOfBook, x => x.SellerId == currentUserId & x.Id > 0));
        }
    }
}