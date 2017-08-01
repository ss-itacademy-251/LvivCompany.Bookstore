using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LvivCompany.Bookstore.Web.Config;
using Microsoft.Extensions.Configuration;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private AppConfiguration Configuration;
        public HomeController(IOptionsSnapshot<AppConfiguration> configuration)
        {
            Configuration = configuration.Value;
        }
        public IActionResult Index()
        {
           
            return View();
        }
    }
}