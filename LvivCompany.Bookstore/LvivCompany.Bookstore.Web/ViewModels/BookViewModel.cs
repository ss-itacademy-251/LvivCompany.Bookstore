using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities.Models;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class BookViewModel
    {
        [Required(ErrorMessage = "At least one author is required")]
        public List<Author> Authors { get; set; } = new List<Author>();
        [Required(ErrorMessage = "The title is required")]
        [Display(Name = "Book Title")]
        public string Name { get; set; }
        public short Year { get; set; }
        [Display(Name = "Number of pages")]
        [Required(ErrorMessage = "The number of book pages is required")]
        public int NumberOfPages { get; set; }
        [Display(Name = "Number of available items")]
        [Required(ErrorMessage = "The number of available items is required")]
        public int Amount { get; set; }
        public string Description { get; set; }
        public string PublisherName { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "The price is required"), Range(1, 1000, ErrorMessage = "Min price: 1$, max price: 1000$")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
