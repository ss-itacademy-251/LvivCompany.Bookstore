using System;
using System.Collections.Generic;
using System.Text;

namespace LvivCompany.Bookstore.Entities.Models.ClassTest
{
    public class BookTest: BaseEntity
    {
        public string SellerName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Year { get; set; }

        public string Publisher { get; set; }

        public int NumberOfPages { get; set; }

        public string Category { get; set; }

        public int Amount { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public string Author { get; set; }

    }
}
