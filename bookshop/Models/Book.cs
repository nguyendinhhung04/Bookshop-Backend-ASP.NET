using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.Models
{
    [Table("BOOK")]
    public class Book
    {

        public Book(int id, string title, int numberOfPages, int onSale)
        {
            this.Id = id;
            this.title = title;
            this.numberOfPages = numberOfPages;
            this.onSale = onSale;
        }

        public int Id { get; private set; } // Primary key property
        public string title { get; private set; }
        public int numberOfPages { get; private set; }
        public int onSale { get; private set; }
    }
}
