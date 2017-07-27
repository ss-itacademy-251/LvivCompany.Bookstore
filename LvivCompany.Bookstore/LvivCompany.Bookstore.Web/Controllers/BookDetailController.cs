using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;
using AutoMapper;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookDetailController : Controller
    {
        private IRepo<Book> _bookRepo;

        public BookDetailController(IRepo<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<IActionResult> Index()
        {
            var book = await _bookRepo.GetAsync(8);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, BookDetailViewModel>()
                .ForMember(bv => bv.Author, b => b.MapFrom(
                a => (a.BookAuthors.First().Author.FirstName + " "+  a.BookAuthors.First().Author.LastName)  ))
                .ForMember(bv => bv.Category, b => b.MapFrom(
                a => a.Category.Name))
                .ForMember(bv => bv.Publisher, b => b.MapFrom(
                a => a.Publisher.Name));
            });

            IMapper mapper = config.CreateMapper();
            var model = mapper.Map<Book, BookDetailViewModel>(book);
            return View(model);
          
        }
    }
}