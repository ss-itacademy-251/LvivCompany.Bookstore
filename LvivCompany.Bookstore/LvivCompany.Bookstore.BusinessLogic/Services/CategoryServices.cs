using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace LvivCompany.Bookstore.BusinessLogic
{
    public class CategoryServices
    {
        private IRepo<Category> _categoryRepo;
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookViewModel> _bookmapper;

        public CategoryServices(IRepo<Category> categoryRepo, IRepo<Book> bookRepo, IMapper<Book, BookViewModel> bookmapper)
        {
            _categoryRepo = categoryRepo;
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }

        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await _categoryRepo.GetAllAsync();
        }
        public HomePageListViewModel GetViewModel(long id)
        {
            List<Book> books = _bookRepo.Get(x => x.CategoryId.Equals(id)).ToList();
            return new HomePageListViewModel() { Books = _bookmapper.Map(books) };
        }
    }
}