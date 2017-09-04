using System.Threading.Tasks;
using LvivCompany.Bookstore.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class SearchController : Controller
    {
        private SearchServices services;

        public SearchController(SearchServices services)
        {
            this.services = services;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            return View(await services.GetViewModelForHomePage(searchText));
        }
    }
}