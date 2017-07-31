using System.ComponentModel.DataAnnotations;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class AuthorViewModel
    {
        [Required(ErrorMessage = "The author's first name is required")]
        [Display (Name ="First Name") ]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The author's last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
