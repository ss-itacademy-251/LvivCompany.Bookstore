using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LvivCompany.Bookstore.BusinessLogic.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("[0-9]{10}")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        public IFormFile Photo { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
    }
}