using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}
