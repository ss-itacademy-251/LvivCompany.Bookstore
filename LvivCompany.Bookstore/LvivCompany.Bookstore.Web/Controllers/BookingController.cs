using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class BookingController : Controller
    {

        private IBookingRepo<Book> db;
        public BookingController(IBookingRepo<Book> db)
        {
            this.db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }
    }
}
