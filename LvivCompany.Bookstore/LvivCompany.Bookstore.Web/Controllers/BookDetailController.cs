using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;
using LvivCompany.Bookstore.Web.Mapper;


namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookDetailController : Controller
    {
        private IRepo<Book> _bookRepo;
        private IMapper<Book,BookDetailViewModel> _bookmapper;

        public BookDetailController(IMapper<Book, BookDetailViewModel> bookmapper,IRepo<Book> bookRepo )
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }

        public async Task<IActionResult> Index(int id)
        {
      
            var book = await _bookRepo.GetAsync(id);
            return View("Index", _bookmapper.Map(book));
        }
    }
}