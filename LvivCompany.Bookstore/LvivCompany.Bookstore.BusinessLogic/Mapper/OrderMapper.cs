using LvivCompany.Bookstore.Entities;
using System;
using System.Collections.Generic;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;

namespace LvivCompany.Bookstore.BusinessLogic.Mapper
{
    public class OrderMapper : IMapper<OrderDetail, OrderViewModel>
    {
        public OrderDetail Map(OrderViewModel entity)
        {
            throw new NotImplementedException();
        }

        public OrderViewModel Map(OrderDetail entity)
        {
            OrderViewModel model = new OrderViewModel
            {
                BookId = entity.Book.Id,
                BookName = entity.Book.Name,
                Amount = entity.Amount,
                Price = entity.Book.Price,
                Category = entity.Book.Category.Name,
                ImageURL = entity.Book.ImageUrl
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

        public List<OrderDetail> Map(List<OrderViewModel> entity)
        {
            throw new NotImplementedException();
        }
    }
}