using bookshop.Models;

namespace bookshop
{
    public class TempData
    {
        public List<Book> books;

        public TempData() {
            this.books = new List<Book>();
            for (int i = 0; i <= 10; i++)
            {
                books.Add(new Book(
                    i,
                    "Book " + i.ToString(), 
                    i*50,
                    i*7%2
                    ));
            }
        }
    }
}
