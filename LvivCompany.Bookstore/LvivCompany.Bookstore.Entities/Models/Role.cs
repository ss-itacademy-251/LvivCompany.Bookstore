using Microsoft.AspNetCore.Identity;

namespace LvivCompany.Bookstore.Entities
{
    public class Role : IdentityRole<long>
    {
        public Role() : base()
        {
        }

        public Role(string roleName)
        {
            Name = roleName;
        }
    }
}
