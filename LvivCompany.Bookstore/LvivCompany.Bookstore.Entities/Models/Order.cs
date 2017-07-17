using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities
{
    public class Order : BaseEntity
    {
        public long CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public long StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}
