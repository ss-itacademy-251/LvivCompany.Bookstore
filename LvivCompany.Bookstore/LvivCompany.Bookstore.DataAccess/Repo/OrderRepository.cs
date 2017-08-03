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

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Orders
                    .Include(x => x.Status)
                    .ToListAsync();
        }

        public async Task<Order> GetAsync(long id)
        {
            return await context.Orders
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Order order)
        {
            await context.Orders.AddAsync(order);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
            return await Task.FromResult<Order>(null);
        }

        public async Task DeleteAsync(long id)
        {
            Order order = await context.Orders.FindAsync(id);
            if (order != null)
                context.Orders.Remove(order);
        }

        public async Task<Order> DeleteAsync(Order order)
        {
            context.Entry(order).State = EntityState.Deleted;
            return await Task.FromResult<Order>(null);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
