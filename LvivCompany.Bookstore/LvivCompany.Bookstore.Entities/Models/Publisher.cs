using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }
    }
}
