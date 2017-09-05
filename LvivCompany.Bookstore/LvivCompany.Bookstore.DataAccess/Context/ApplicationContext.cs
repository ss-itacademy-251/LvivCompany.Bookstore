﻿using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.DataAccess
{
    public class ApplicationContext : IdentityDbContext<User, Role, long>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}