using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.DataAccess;
using System.ComponentModel.DataAnnotations;



namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class HomePageListViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Display(Name = "Category")]
        public decimal Category { get; set; }
    }
}
