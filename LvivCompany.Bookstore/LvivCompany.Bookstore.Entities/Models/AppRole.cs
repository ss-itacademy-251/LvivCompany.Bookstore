using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities.Models
{
   public class AppRole:IdentityRole<Int64>
    {
        public AppRole() { }
        public AppRole(string name)
         : this()
        {
           this.Name = name;
        }
        public string ShowingItsPossible { get; set; }
    }
}
