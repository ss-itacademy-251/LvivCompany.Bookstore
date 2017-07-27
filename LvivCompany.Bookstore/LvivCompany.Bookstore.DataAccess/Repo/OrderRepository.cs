using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public class OrderRepository : IRepo<Order>
    {
        private BookStoreContext context;

        public OrderRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return context.Orders;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Orders
                    .Include(x => x.Status)
                    .ToListAsync();
        }

        public Order Get(long id)
        {
            return context.Orders.Find(id);
        }

        public async Task<Order> GetAsync(long id)
        {
            return await context.Orders
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Create(Order order)
        {
            context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Order order = context.Orders.Find(id);
            if (order != null)
                context.Orders.Remove(order);
        }

        public void Delete(Order order)
        {
            context.Entry(order).State = EntityState.Deleted;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
