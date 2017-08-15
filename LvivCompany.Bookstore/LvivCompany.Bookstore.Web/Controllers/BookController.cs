using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookController : Controller
    {
        private IRepo<Book> repoBook;
        private IRepo<Category> repoCategory;
        private IMapper<Book, BookViewModel> bookmapper;
        private IConfiguration configuration;

        public BookController(IRepo<Book> repoBook, IRepo<Category> repoCategory, IMapper<Book, BookViewModel> bookmapper, IConfiguration configuration)
        {
            this.repoBook = repoBook;
            this.repoCategory = repoCategory;
            this.bookmapper = bookmapper;
            this.configuration = configuration;
        }

        public async Task<IActionResult> BookPage(long id)
        {
            var book = await repoBook.GetAsync(id);
            return View("BookPage", bookmapper.Map(book));
        }

        [Authorize(Roles ="Seller")]
        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            BookViewModel model = new BookViewModel();
            await PopulateCategoriesSelectList(model);
            model.ImageUrl = UploadFile.defaultBookImage;
            return View("AddBook", model);
        }
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = bookmapper.Map(model);
                if (model.Image != null)
                {
                    book.ImageUrl = await UploadFile.RetrieveFilePath(model.Image, configuration);
                }
                else
                {
                    book.ImageUrl = UploadFile.defaultBookImage;
                }

                await repoBook.CreateAsync(book);
                return RedirectToAction("Index", "Home");
            }

            model.ImageUrl = UploadFile.defaultBookImage;
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