using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.DataAccess;
using System.Threading.Tasks;
using AutoMapper;
using LvivCompany.Bookstore.Web.ViewModels;
using System.Linq;
using System.Collections.Generic;
using LvivCompany.Bookstore.Web.Mapper;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepo<Book> _bookRepo;
        List<BookInfo> booklist;
        BookDetailViewModel model;

        public HomeController(IRepo<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<IActionResult> Index()
        {
            var book = await _bookRepo.GetAllAsync();
            IMapper mapper = Mappers.MapperForBookInfo().CreateMapper();
            booklist = mapper.Map<IEnumerable<Book>, List<BookInfo>>(book);
            return View("Index", new HomePageListViewModel() { Books = booklist });
        }
    }
}