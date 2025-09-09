using Microsoft.EntityFrameworkCore;
namespace bookshop.Models
{
    public class BookShopContext : DbContext
    {
        public BookShopContext(DbContextOptions<BookShopContext> options) : base(options)
        {

        }

        public DbSet<Book> Book { get; set; } = null!;
    }
}
