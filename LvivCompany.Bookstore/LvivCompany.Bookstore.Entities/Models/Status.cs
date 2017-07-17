using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
    }
}
