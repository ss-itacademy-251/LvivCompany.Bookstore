using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class StatusRepository : Repo<Status>
    {
        public StatusRepository(BookStoreContext context) : base(context)
        {
        }

        public override Task<Status> GetAsync(long id)
        {
            return context.Statuses
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}