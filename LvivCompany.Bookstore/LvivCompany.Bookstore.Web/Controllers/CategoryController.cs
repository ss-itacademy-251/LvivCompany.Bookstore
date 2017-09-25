using LvivCompany.Bookstore.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryServices services;

        public CategoryController(CategoryServices services)
        {
            this.services = services;
        }

        public IActionResult Index(long id)
         {
            return View(services.GetViewModel(id));
        }
    }
}