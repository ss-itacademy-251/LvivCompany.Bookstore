using System.Collections.Generic;

namespace LvivCompany.Bookstore.BusinessLogic.ViewModels
{
    public class ListOrdersViewModel
    {
        public List<OrderViewModel> Models { get; set; }

        public decimal TotalPrice { get; set; }
    }
}