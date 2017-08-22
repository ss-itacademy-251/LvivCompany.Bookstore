using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int Amount { get; set; }

        public long BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public long OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        
    }
}
