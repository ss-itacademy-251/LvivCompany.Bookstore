using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.DataAccess;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LvivCompany.Bookstore.Web.Config;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using LvivCompany.Bookstore.Web.ViewModels;
using System.Linq;
using System.Collections.Generic;
using LvivCompany.Bookstore.Web.Mapper;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private AppConfiguration Configuration;
        public HomeController(IOptionsSnapshot<AppConfiguration> configuration)
        {
            Configuration = configuration.Value;
        }

        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookInfo> _bookmapper;

        public HomeController(IRepo<Book> bookRepo, IMapper<Book, BookInfo> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }

        public async Task<IActionResult> Index()
        {
            var book = await _bookRepo.GetAllAsync();
            return View(new HomePageListViewModel() { Books = _bookmapper.Map(book)});
        }
    }
}