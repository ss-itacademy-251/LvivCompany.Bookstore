using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.DataAccess.IRepo;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class SellerController : Controller
    {

        private IRepo<Book> db;
        public SellerController()
        {
            db = new BookRepository();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }
    }
}
