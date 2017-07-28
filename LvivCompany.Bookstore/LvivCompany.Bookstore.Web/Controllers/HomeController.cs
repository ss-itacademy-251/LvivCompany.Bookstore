using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.DataAccess;
using System.Threading.Tasks;
using AutoMapper;
using LvivCompany.Bookstore.Web.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepo<Book> _bookRepo;
        List<BookDetailViewModel> booklist=new List<BookDetailViewModel>();
        BookDetailViewModel model;

        public HomeController(IRepo<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<IActionResult> Index()
        {
            var book = await _bookRepo.GetAllAsync();
            book.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, BookDetailViewModel>()
                .ForMember(bv => bv.Author, b => b.MapFrom(
                a => (a.BookAuthors.First().Author.FirstName + " " + a.BookAuthors.First().Author.LastName)))
                .ForMember(bv => bv.Category, b => b.MapFrom(
                a => a.Category.Name))
                .ForMember(bv => bv.Publisher, b => b.MapFrom(
                a => a.Publisher.Name));
            });

            /* IRepo<Book> set = null;
             if (book != null)
             {
                 foreach (Book item in book)
                 {
                     set.Add(Mapper.Map<Book, BookDetailViewModel>(item));
                 }
             }
             return set;*/
           
            IMapper mapper = config.CreateMapper();
           
            foreach (Book b in book.ToList())
            {
                model = mapper.Map<Book, BookDetailViewModel>(b);
                booklist.Add(model);

            }
                return View("Index", booklist);
            
        }
    }
}