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
        [Display(Name = "Books")]
        public List<BookInfo> Books { get; set; }
    }

    public class BookInfo
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
