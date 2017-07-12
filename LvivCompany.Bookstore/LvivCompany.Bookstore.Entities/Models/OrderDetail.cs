using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }


    }
}
