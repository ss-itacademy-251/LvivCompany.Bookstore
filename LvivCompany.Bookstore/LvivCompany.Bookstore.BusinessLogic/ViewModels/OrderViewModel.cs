namespace LvivCompany.Bookstore.BusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public string BookName { get; set; }
        public long BookId { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImageURL { get; set; }
        public decimal TotalPrice => Quantity * Price;
        public long OrderId { get; set; }
    }
}