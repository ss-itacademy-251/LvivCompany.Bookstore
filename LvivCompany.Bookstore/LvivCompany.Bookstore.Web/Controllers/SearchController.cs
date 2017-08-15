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
        private IRepo<Author> _authorRepo;
        private IMapper<Book, BookViewModel> _bookmapper;

        public SearchController(IRepo<Book> bookRepo, IRepo<Author> authorRepo, IMapper<Book, BookViewModel> bookmapper)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _bookmapper = bookmapper;
        }
   
        public async Task<IActionResult> Index(string SearchText)
        {
            List<Book> book = (await _bookRepo.GetAllAsync()).ToList() ;
            Author author = new Author();
            if (!string.IsNullOrEmpty(SearchText))
            {
                var result = book.Where(s => s.Name.Contains(SearchText) );
                return View(new HomePageListViewModel() { Books = _bookmapper.Map(result.ToList()) });
            }
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(book) });
        }
    }
}
