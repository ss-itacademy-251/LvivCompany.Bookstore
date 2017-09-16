using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class OrderViewModel
    {
        public int Amount { get; set; }

        public long Id { get; set; }

        public string BookName { get; set; }

        public long BookId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string ImageURL { get; set; }

        public decimal TotalPrice => Quantity * Price;

        public long OrderId { get; set; }
    }
}
