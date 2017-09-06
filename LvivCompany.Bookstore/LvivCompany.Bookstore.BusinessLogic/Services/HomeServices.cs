using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.BusinessLogic
{
    public class HomeServices
    {
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookViewModel> _bookmapper;

        public HomeServices(IRepo<Book> bookRepo, IMapper<Book, BookViewModel> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }

        public async Task<HomePageListViewModel> GetViewModelForHomePage()
        {
            List<Book> book = (await _bookRepo.GetAllAsync()).ToList();
            return new HomePageListViewModel() { Books = _bookmapper.Map(book) };
        }

        public HomePageListViewModel GetSellersBook(long userId)
        {
            var currentUserId = userId;
            List<Book> book = _bookRepo.Get(x => x.SellerId == currentUserId).ToList();
            return new HomePageListViewModel() { Books = _bookmapper.Map(book) };
        }
        public async Task<HomePageListViewModel> GetBooksPageAsync(int page ,int CountOfBook)
        {
            if (page == 0)
            {
                page = 1;
            }

            List<Book> book = (await _bookRepo.GetPageAsync(x => x.Id > 0, CountOfBook, page)).ToList();

            if (book.Count < CountOfBook - 1)
            {
                return new HomePageListViewModel() { Books = _bookmapper.Map(book), PageNumber = page, ExistNext = false };
            }

            return new HomePageListViewModel() { Books = _bookmapper.Map(book), PageNumber = page, ExistNext = true };
        }
    }
}