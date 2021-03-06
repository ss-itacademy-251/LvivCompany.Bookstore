﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.DataAccess;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private const int CountOfBook = 8;
        private IRepo<Book> _bookRepo;
        private IMapper<Book, BookViewModel> _bookmapper;

        public HomeController(IRepo<Book> bookRepo, IMapper<Book, BookViewModel> bookmapper)
        {
            _bookRepo = bookRepo;
            _bookmapper = bookmapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page)
        {
            if (page == 0)
            {
                page = 1;
            }

            List<Book> book = (await _bookRepo.GetPageAsync(x => x.Id > 0, CountOfBook, page)).ToList();

            if (book.Count < CountOfBook)
            {
                return View(model: new HomePageListViewModel() { Books = _bookmapper.Map(book), PageNumber = page, ExistNext = false });
            }

            return View(model: new HomePageListViewModel() { Books = _bookmapper.Map(book), PageNumber = page, ExistNext = true });
        }
    }
}