using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class HomePageListViewModel
    {
        [Display(Name = "Books")]
        public List<BookViewModel> Books { get; set; }
    }
}