using Microsoft.EntityFrameworkCore;
namespace bookshop.Models
{
    public class BookShopContext : DbContext
    {
        public BookShopContext(DbContextOptions<BookShopContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; } = null!;
    }
}
