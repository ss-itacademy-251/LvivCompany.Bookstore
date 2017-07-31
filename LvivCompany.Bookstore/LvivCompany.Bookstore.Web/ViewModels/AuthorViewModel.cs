using System.ComponentModel.DataAnnotations;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class AuthorViewModel
    {
        [Required(ErrorMessage = "The author's first name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The author's last name is required")]
        public string LastName { get; set; }
    }
}
