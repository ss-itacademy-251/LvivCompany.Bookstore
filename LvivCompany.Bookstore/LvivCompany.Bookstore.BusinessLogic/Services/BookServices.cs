using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.BusinessLogic
{
    public class BookServices
    {
        private IRepo<Book> repoBook;
        private IRepo<Category> repoCategory;
        private IMapper<Book, BookViewModel> bookmapper;
        private IMapper<Book, EditBookViewModel> editBookMapper;
        private IConfiguration configuration;

        public BookServices(IRepo<Book> repoBook, IRepo<Category> repoCategory, IMapper<Book, BookViewModel> bookmapper, IMapper<Book, EditBookViewModel> editBookMapper, IConfiguration configuration)
        {
            this.repoBook = repoBook;
            this.repoCategory = repoCategory;
            this.bookmapper = bookmapper;
            this.editBookMapper = editBookMapper;
            this.configuration = configuration;
        }

        public async Task<BookViewModel> GetViewModelForBookPageAsync(long id)
        {
            var book = await repoBook.GetAsync(id);
            return bookmapper.Map(book);
        }

        public async Task<BookViewModel> GetViewModelForAddBookPageAsync(BookViewModel model)
        {
            await PopulateCategoriesSelectListAsync(model);
            model.ImageUrl = UploadFile.defaultBookImage;
            return model;
        }

        public async Task AddBookAsync(BookViewModel model, long sellerId)
        {
                Book book = bookmapper.Map(model);
                book.SellerId = sellerId;
                if (model.Image != null)
                {
                    book.ImageUrl = await UploadFile.RetrieveFilePath(model.Image, configuration);
                }
                else
                {
                    book.ImageUrl = UploadFile.defaultBookImage;
                }

                await repoBook.CreateAsync(book);
        }

        public async Task<EditBookViewModel> GetViewModelForEditBookPageAsync(long id)
        {
            Book book = await repoBook.GetAsync(id);
            var model = editBookMapper.Map(book);
            await PopulateCategoriesSelectListAsync(model);
            return model;
        }

        public async Task EditBookAsync(long id, EditBookViewModel model)
        {
                Book book = await repoBook.GetAsync(id);
                book = editBookMapper.Map(model, book);
                if (model.Image != null)
                {
                    book.ImageUrl = await UploadFile.RetrieveFilePath(model.Image, configuration);
                }

                await repoBook.UpdateAsync(book);
        }

        public async Task PopulateCategoriesSelectListAsync(EditBookViewModel model)
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