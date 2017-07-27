using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public class OrderDetailRepository : IRepo<OrderDetail>
    {
        private BookStoreContext context;

        public OrderDetailRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await context.OrderDetails
                    .Include(x => x.Book)
                    .ThenInclude(x=>x.Category)
                    .Include(x => x.Book)
                    .ThenInclude(x=>x.Publisher)
                    .Include(x => x.Book)
                    .ThenInclude(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.Order)
                    .ThenInclude(x => x.Status)
                    .ToListAsync();
        }

        public async Task<OrderDetail> GetAsync(long id)
        {
            return await context.OrderDetails
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(OrderDetail orderDetail)
        {
            await context.OrderDetails.AddAsync(orderDetail);
        }

        public async Task<OrderDetail> UpdateAsync(OrderDetail orderDetail)
        {
            context.Entry(orderDetail).State = EntityState.Modified;
            return await Task.FromResult<OrderDetail>(null);
        }

        public async Task DeleteAsync(long id)
        {
            OrderDetail orderDetail = await context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
                context.OrderDetails.Remove(orderDetail);
        }

        public async Task<OrderDetail> DeleteAsync(OrderDetail orderDetail)
        {
            context.Entry(orderDetail).State = EntityState.Deleted;
            return await Task.FromResult<OrderDetail>(null);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
