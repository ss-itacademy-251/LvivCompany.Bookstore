using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class BookViewModel
    {
        public string Name { get; set; }
        public DateTime Year { get; set; }
        [Display(Name = "Number of pages")]
        public int NumberOfPages { get; set; }
        [Display(Name = "Number of items")]
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
