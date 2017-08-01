using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.Web.Config
{
    public class AppConfiguration
    {
         public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string BookStore { get; set; }
    }
}
