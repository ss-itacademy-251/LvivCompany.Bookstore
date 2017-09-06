using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LvivCompany.Bookstore.BusinessLogic.ViewModels
{
    public class EditBookViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "The title is required")]
        [StringLength(100)]
        [Display(Name = "Book Title")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Publication year")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Number of pages")]
        public int NumberOfPages { get; set; }

        [Required]
        [Display(Name = "Number of available items")]
        public int Amount { get; set; }

        [StringLength(10000)]
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The category is required")]
        [Display(Name = "Category")]
        public long CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public string Category { get; set; }
    }
}