using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;
using AutoMapper;
using LvivCompany.Bookstore.Web.Mapper;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookDetailController : Controller
    {
        private IRepo<Book> _bookRepo;

        public BookDetailController(IRepo<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<IActionResult> Index(int id)
        {
            var book = await _bookRepo.GetAsync(id);

            IMapper mapper = Mappers.MapperForBook().CreateMapper();
            var model = mapper.Map<Book, BookDetailViewModel>(book);
            return View(model);
          
        }
    }
}