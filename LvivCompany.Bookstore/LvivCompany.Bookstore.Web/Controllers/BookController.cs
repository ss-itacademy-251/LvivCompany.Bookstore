using System.Threading.Tasks;
using LvivCompany.Bookstore.BusinessLogic;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookController : Controller
    {
        private UserManager<User> userManager;
        private BookServices services;

        public BookController(UserManager<User> userManager, BookServices services)
        {
            this.userManager = userManager;
            this.services = services;
        }

        public async Task<IActionResult> BookPage(long id)
        {
            return View("BookPage", await services.GetViewModelForBookPageAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            BookViewModel model = new BookViewModel();
            return View("AddBook", await services.GetViewModelForAddBookPageAsync(model));
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                await services.AddBookAsync(model, (await userManager.GetUserAsync(HttpContext.User)).Id);
                return RedirectToAction("Index", "Home");
            }

            return View(await services.GetViewModelForAddBookPageAsync(model));
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(long id)
        {
            return View("EditBook", await services.GetViewModelForEditBookPageAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(long id, EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                await services.EditBookAsync(id, model);
                return RedirectToAction("Index", "Home");
            }

            await services.PopulateCategoriesSelectListAsync(model);
            return View(model);
        }
    }
}