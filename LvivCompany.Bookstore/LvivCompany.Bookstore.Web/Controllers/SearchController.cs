using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace LvivCompany.Bookstore.Web.Controllers
{
    public class SearchController : Controller
    {
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookViewModel> _bookmapper;
        //private string noResult="no result";

        public SearchController(IRepo<Book> bookRepo, IMapper<Book, BookViewModel> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }

        public async Task<IActionResult> Index(string SearchText)
        {
                var result = (await ((BookRepository)_bookRepo).GetBySearch(SearchText)).ToList();
                return View(new HomePageListViewModel() { Books = _bookmapper.Map(result) });
        }
    }
}
