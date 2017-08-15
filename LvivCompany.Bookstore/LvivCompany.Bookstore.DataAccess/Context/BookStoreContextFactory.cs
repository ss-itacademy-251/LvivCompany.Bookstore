using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LvivCompany.Bookstore.DataAccess
{
    public class BookStoreContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BookStoreContext>();
            builder.UseSqlServer(@"Server=DESKTOP-ANNA\SQLEXPRESS;Database=bookstoredb;Trusted_Connection=True;");
            return new BookStoreContext(builder.Options);
        }
    }
}