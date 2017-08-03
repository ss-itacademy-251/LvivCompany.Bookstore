using LvivCompany.Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.DataAccess.Repo
{
    public class PublisherRepository : Repo<Publisher>
    {
        public PublisherRepository(BookStoreContext context) : base(context)
        {
        }

        public override Task<Publisher> GetAsync(long id)
        {
            return context.Publishers
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}