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

        public IEnumerable<OrderDetail> GetAll()
        {
            return context.OrderDetails;
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

        public OrderDetail Get(long id)
        {
            return context.OrderDetails.Find(id);
        }

        public async Task<OrderDetail> GetAsync(long id)
        {
            return await context.OrderDetails
                .FirstOrDefaultAsync(x => x.Id == id);
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
