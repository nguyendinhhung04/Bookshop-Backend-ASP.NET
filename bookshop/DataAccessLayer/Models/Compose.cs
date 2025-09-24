using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.DataAccessLayer.Models
{
    [Table("COMPOSE")]
    public class Compose
    {
        public Compose() { }

        public int ID { get; set; } // Primary key property
        [ForeignKey("Book")]
        public int BOOK_ID { get; set; }
        [ForeignKey("Author")]
        public int AUTHOR_ID { get; set; }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
