using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private IMapper<Book, EditBookViewModel> editBookMapper;
        private UserManager<User> userManager;
        private IConfiguration configuration;

        public BookController(IRepo<Book> repoBook, IRepo<Category> repoCategory, IMapper<Book, BookViewModel> bookmapper, IMapper<Book, EditBookViewModel> editBookMapper, IConfiguration configuration, UserManager<User> userManager)
        {
            this.repoBook = repoBook;
            this.repoCategory = repoCategory;
            this.bookmapper = bookmapper;
            this.editBookMapper = editBookMapper;
            this.userManager = userManager;
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
                book.SellerId = (await userManager.GetUserAsync(HttpContext.User)).Id;
                if (model.Image != null)
                {
                    book.ImageUrl = await UploadFile.RetrieveFilePath(model.Image, configuration);
                }
                else
                {
                    book.ImageUrl = UploadFile.defaultBookImage;
                }

                await repoBook.CreateAsync(book);
                return RedirectToAction("SellersBook", "Home");
            }

            model.ImageUrl = UploadFile.defaultBookImage;
            await PopulateCategoriesSelectList(model);
            return View(model);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet]
        public async Task<IActionResult> EditBook(long id)
        {
            Book book = await repoBook.GetAsync(id);
            var model = editBookMapper.Map(book);
            await PopulateCategoriesSelectList(model);
            return View("EditBook", model);
        }

        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> EditBook(long id, EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = await repoBook.GetAsync(id);
                book = editBookMapper.Map(model, book);
                if (model.Image != null)
                {
                    book.ImageUrl = await UploadFile.RetrieveFilePath(model.Image, configuration);
                }
                await repoBook.UpdateAsync(book);
                return RedirectToAction("SellersBook", "Home");
            }
            await PopulateCategoriesSelectList(model);
            return View(model);
        }

        private async Task PopulateCategoriesSelectList(EditBookViewModel model)
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