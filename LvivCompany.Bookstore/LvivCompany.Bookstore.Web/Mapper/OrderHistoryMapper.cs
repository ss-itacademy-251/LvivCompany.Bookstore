using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class OrderHistoryMapper : IMapper<OrderDetail, OrderHistoryViewModel>
    {
        private UserManager<User> _userManager;

        public OrderHistoryMapper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public OrderDetail Map(OrderHistoryViewModel model)
        {
            OrderDetail orderDetail = new OrderDetail();
            return orderDetail;
        }

        public OrderHistoryViewModel Map(OrderDetail orderDetail)
        {
            OrderHistoryViewModel model = new OrderHistoryViewModel();
            model.BookName = orderDetail.Book.Name;
            model.Amount = orderDetail.Amount;
            model.TotalPrice = orderDetail.Order.TotalPrice;
            model.OrderDateTime = orderDetail.Order.OrderDate;
            model.Seller = GetUser(orderDetail.Book.SellerId).Result.UserName;
            model.Customer = GetUser(orderDetail.Order.CustomerId).Result.UserName;
            return model;
        }
        public OrderDetail Map(OrderHistoryViewModel model, OrderDetail orderDetail)
        {
            return orderDetail;
        }
        public List<OrderHistoryViewModel> Map(List<OrderDetail> entity)
        {

            List<OrderHistoryViewModel> models = new List<OrderHistoryViewModel>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }
            return models;

        }
        public List<OrderDetail> Map(List<OrderHistoryViewModel> entity)
        {

            List<OrderDetail> models = new List<OrderDetail>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }
            return models;

        }
        public async Task<User> GetUser(long id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());

            return user;
        }

    }
}
