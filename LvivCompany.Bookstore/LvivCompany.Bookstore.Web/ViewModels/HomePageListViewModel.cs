using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.DataAccess;

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
        public byte[] Image { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
