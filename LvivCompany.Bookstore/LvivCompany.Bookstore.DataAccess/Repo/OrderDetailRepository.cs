using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class OrderDetailRepository : IRepo<OrderDetail>
    {
        private BookStoreContext context;

        public OrderDetailRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return context.OrderDetails;
        }

        public OrderDetail Get(long id)
        {
            return context.OrderDetails.Find(id);
        }

        public void Create(OrderDetail orderDetail)
        {
            context.OrderDetails.Add(orderDetail);
        }

        public void Update(OrderDetail orderDetail)
        {
            context.Entry(orderDetail).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            OrderDetail orderDetail = context.OrderDetails.Find(id);
            if (orderDetail != null)
                context.OrderDetails.Remove(orderDetail);
        }

        public void Delete(OrderDetail orderDetail)
        {
            context.Entry(orderDetail).State = EntityState.Deleted;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
