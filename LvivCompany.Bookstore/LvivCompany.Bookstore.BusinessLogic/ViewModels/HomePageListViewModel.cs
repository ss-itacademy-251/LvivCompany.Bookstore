using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LvivCompany.Bookstore.BusinessLogic.ViewModels
{
    public class HomePageListViewModel
    {
        [Display(Name = "Books")]
        public List<BookViewModel> Books { get; set; }

        public int PageNumber { get;  set; }

        public bool ExistNext { get; set; }
    }
}