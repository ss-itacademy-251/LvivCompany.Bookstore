using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class OrderDetailRepository : Repo<OrderDetail>
    {
        public OrderDetailRepository(BookStoreContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await context.OrderDetails
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Publisher)
                    .Include(x => x.Book)
                    .ThenInclude(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.Order)
                    .ThenInclude(x => x.Status)
                    .ToListAsync();
        }

        public override Task<OrderDetail> GetAsync(long id)
        {
            return context.OrderDetails
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<OrderDetail>> GetAllAsyncByCustomerId(long id)
        {
            IEnumerable<OrderDetail> result = (from orderdetail in context.OrderDetails where orderdetail.Order.CustomerId == id select orderdetail).Include(x => x.Book).Include(x => x.Order);
            return await Task.FromResult<IEnumerable<OrderDetail>>(result);

        }
        public async Task<IEnumerable<OrderDetail>> GetAllAsyncBySellerId(long id)
        { 
            IEnumerable<OrderDetail> result = (from orderdetail in context.OrderDetails where orderdetail.Book.SellerId == id select orderdetail).Include(x => x.Book).Include(x => x.Order);
            return await Task.FromResult<IEnumerable<OrderDetail>>(result);
        }
    }
}