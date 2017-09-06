using System;

namespace LvivCompany.Bookstore.BusinessLogic.ViewModels
{
    public class OrderHistoryViewModel
    {
        public string Customer { get; set; }

        public string BookName { get; set; }

        public string Seller { get; set; }

        public int Amount { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDateTime { get; set; }
    }
}