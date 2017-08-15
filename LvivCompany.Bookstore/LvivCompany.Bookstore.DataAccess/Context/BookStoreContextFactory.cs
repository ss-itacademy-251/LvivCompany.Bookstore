using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LvivCompany.Bookstore.DataAccess
{
    public class BookStoreContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BookStoreContext>();
            builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=IdDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new BookStoreContext(builder.Options);
        }
    }
}