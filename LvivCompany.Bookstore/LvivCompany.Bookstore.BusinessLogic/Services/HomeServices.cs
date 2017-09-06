using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<HomePageListViewModel> GetBooksPageAsync(int page, int CountOfBook, Expression<Func<Book, bool>> filter)
        {
            if (page == 0)
            {
                page = 1;
            }

            List<Book> book = (await _bookRepo.GetPageAsync(filter, CountOfBook, page)).ToList();

            if (book.Count < CountOfBook - 1)
            {
                return new HomePageListViewModel() { Books = _bookmapper.Map(book), PageNumber = page, ExistNext = false };
            }

            return new HomePageListViewModel() { Books = _bookmapper.Map(book), PageNumber = page, ExistNext = true };
        }
    }
}