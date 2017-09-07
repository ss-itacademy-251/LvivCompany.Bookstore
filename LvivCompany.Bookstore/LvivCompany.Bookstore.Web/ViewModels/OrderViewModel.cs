using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.Web.ViewModels
{
    public class OrderViewModel
    {
        public long Id { get; set; }

        public string BookName { get; set; }

        public long BookId { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string ImageURL { get; set; }

        public decimal TotalPrice => Amount * Price;

        public long OrderId { get; set; }
    }
}
