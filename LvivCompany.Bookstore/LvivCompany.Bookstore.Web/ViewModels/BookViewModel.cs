using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class BookViewModel : EditBookViewModel
    {
        public List<AuthorViewModel> Authors { get; set; }

        [Required(ErrorMessage = "The publisher name is required")]
        [StringLength(100)]
        [Display(Name = "Publisher")]
        public string PublisherName { get; set; }

        public BookViewModel()
        {
            Authors = new List<AuthorViewModel>
            {
                new AuthorViewModel()
            };
        }
    }
}