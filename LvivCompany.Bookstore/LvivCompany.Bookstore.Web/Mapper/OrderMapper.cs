using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class OrderMapper : IMapper<OrderDetail, OrderViewModel>
    {
        public OrderDetail Map(OrderViewModel model)
        {
            OrderDetail entity = new OrderDetail
            {
                Amount = model.Amount,
                BookId = model.BookId,
                OrderId = model.OrderId
            };
            return entity;
        }

        public OrderViewModel Map(OrderDetail entity)
        {
            OrderViewModel model = new OrderViewModel
            {
                BookId = entity.Book.Id,
                BookName = entity.Book.Name,
                Quantity = entity.Amount,
                Price = entity.Book.Price,
                Category = entity.Book.Category.Name,
                ImageURL = entity.Book.ImageUrl,
                Amount = entity.Book.Amount
            };
            return model;
        }

        public OrderDetail Map(OrderViewModel model, OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public List<OrderViewModel> Map(List<OrderDetail> entity)
        {
            List<OrderViewModel> model = new List<OrderViewModel>();
            foreach (var item in entity)
            {
                model.Add(Map(item));
            }

            return model;
        }

        public List<OrderDetail> Map(List<OrderViewModel> model)
        {
            throw new NotImplementedException();
        }
    }
}
