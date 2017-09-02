using System.Threading.Tasks;
using LvivCompany.Bookstore.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private HomeServices services;

        public HomeController(HomeServices services)
        {
            this.services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await services.GetViewModelForHomePage());
        }
    }
}