using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LvivCompany.Bookstore.Entities
{
    public class Book : BaseEntity
    {
        public int SellerId { get; set; }

        public string Name { get; set; }

        public int Description { get; set; }

        public DateTime Year { get; set; }

        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        public int NumberOfPages { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int Amount { get; set; }

        public byte[] Image { get; set; }

        public decimal Price { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
       
    }
}
