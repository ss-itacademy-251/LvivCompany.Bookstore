using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookController : Controller
    {
        private IRepo<Book> repoBook;
        private IRepo<Category> repoCategory;
        private IMapper<Book, BookViewModel> bookmapper;

        public BookController(IRepo<Book> repoBook, IRepo<Category> repoCategory, IMapper<Book, BookViewModel> bookmapper)
        {
            this.repoBook = repoBook;
            this.repoCategory = repoCategory;
            this.bookmapper = bookmapper;
        }

        public async Task<IActionResult> BookPage(long id)
        {
            var book = await repoBook.GetAsync(id);
            return View("BookPage", bookmapper.Map(book));
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            BookViewModel model = new BookViewModel();
            await PopulateCategoriesSelectList(model);
            return View("AddBook", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = bookmapper.Map(model);
                await repoBook.CreateAsync(book);
                return RedirectToAction("Index", "Home");
            }

            await PopulateCategoriesSelectList(model);
            return View(model);
        }

        private async Task PopulateCategoriesSelectList(BookViewModel model)
        {
            var categoryList = await repoCategory.GetAllAsync();
            model.Categories = categoryList.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
        }
    }
}