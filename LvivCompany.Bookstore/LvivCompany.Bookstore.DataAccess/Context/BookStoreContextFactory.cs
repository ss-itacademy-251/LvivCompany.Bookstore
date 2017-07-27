using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.DataAccess
{
    public class BookStoreContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {

        public BookStoreContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BookStoreContext>();
            builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=bookstoredb;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new BookStoreContext(builder.Options);
        }
    }
}
