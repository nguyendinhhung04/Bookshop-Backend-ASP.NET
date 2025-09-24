using bookshop.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace bookshop.DataAccessLayer
{
    public class BookShopContext : DbContext
    {
        public BookShopContext(DbContextOptions<BookShopContext> options) : base(options)
        {

        }

        public DbSet<Book> Book { get; set; } = null!;
    }
}
