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
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Author> Author { get; set; } = null!;
        public DbSet<Compose> Compose { get; set; } = null!;
    }
}
