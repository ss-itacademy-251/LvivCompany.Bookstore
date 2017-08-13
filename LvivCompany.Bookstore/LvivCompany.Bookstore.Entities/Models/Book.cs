using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LvivCompany.Bookstore.Entities
{
    public class Book : BaseEntity
    {
        public long SellerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public long PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        public int NumberOfPages { get; set; }

        public long CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int Amount { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
    }
}