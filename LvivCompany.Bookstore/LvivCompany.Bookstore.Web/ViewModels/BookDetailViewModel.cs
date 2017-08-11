using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class BookDetailViewModel
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int NumberOfPages { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public List<Author> Authors { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
    }

}
