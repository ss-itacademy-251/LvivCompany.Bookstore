using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.DataAccess
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}