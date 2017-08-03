using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class OrderRepository : Repo<Order>
    {
        public OrderRepository(BookStoreContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Orders
                    .Include(x => x.Status)
                    .ToListAsync();
        }

        public override Task<Order> GetAsync(long id)
        {
            return context.Orders
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}