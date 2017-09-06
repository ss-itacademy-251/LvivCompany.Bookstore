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
        private HomeServices services;
        private UserManager<User> _userManager;

        public HomeController(HomeServices services, UserManager<User> userManager)
        {
            this.services = services;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await services.GetViewModelForHomePage());
        }

        [Authorize(Roles = "Seller")]
        [HttpGet]
        public async Task<IActionResult> SellersBook()
        {
            return View(services.GetSellersBook((await _userManager.GetUserAsync(HttpContext.User)).Id));
        }
    }
}