using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.Entities
{
    public class ApplicationContext : IdentityDbContext<User, Role, long>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
