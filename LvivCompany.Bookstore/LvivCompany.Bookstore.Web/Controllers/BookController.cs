using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AccountController> _logger;

        public BookController(IRepo<Book> repoBook, IRepo<Category> repoCategory, IMapper<Book, BookViewModel> bookmapper, IMapper<Book, EditBookViewModel> editBookMapper, IConfiguration configuration, UserManager<User> userManager, ILogger<AccountController> logger)
        {
            this.repoBook = repoBook;
            this.repoCategory = repoCategory;
            this.bookmapper = bookmapper;
            this.editBookMapper = editBookMapper;
            this.userManager = userManager;
            this.configuration = configuration;
            this._logger = logger;
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
            model.ImageUrl = UploadFile.defaultBookImage;
            return View("AddBook", model);
        }

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
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                _logger.LogInformation("Add book {@Book} by user {@User}", new { Name = book.Name, Amount = book.Amount, Category = book.Category }, new { UserName = currentUser.UserName });
                return RedirectToAction("Index", "Home");
            }

            model.ImageUrl = UploadFile.defaultBookImage;
            await PopulateCategoriesSelectList(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(long id)
        {
            Book book = await repoBook.GetAsync(id);
            var model = editBookMapper.Map(book);
            await PopulateCategoriesSelectList(model);
            return View("EditBook", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(long id, EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = await repoBook.GetAsync(id);
                _logger.LogInformation("Edit book {@Book} by user {@User}", new { Name = book.Name, Amount = book.Amount, Category = book.Category });
                book = editBookMapper.Map(model, book);
                if (model.Image != null)
                {
                    book.ImageUrl = await UploadFile.RetrieveFilePath(model.Image, configuration);
                }
                await repoBook.UpdateAsync(book);
                return RedirectToAction("Index", "Home");
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