using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace LvivCompany.Bookstore.Web.Controllers
{
    public class SearchController : Controller
    {
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookInfo> _bookmapper;

        public SearchController(IRepo<Book> bookRepo, IMapper<Book, BookInfo> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }
   
        public async Task<IActionResult> Index(string SearchText)
        {
            List<Book> book = (await _bookRepo.GetAllAsync()).ToList();
            if (!string.IsNullOrEmpty(SearchText))
            {
                var result = book.Where(s => s.Name.Contains(SearchText));
                return View(new HomePageListViewModel() { Books = _bookmapper.Map(result.ToList()) });
            }
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(book) });
        }
    }
}
