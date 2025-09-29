using bookshop.DataAccessLayer.Models;

namespace bookshop.DataAccessLayer
{
    public class GenerateInMemoryDB
    {
        //create data for in-memory database include book table,  category table, author table, compose table
        public static List<Category> Categories = new List<Category>()
        {
            new Category(1, "Science Fiction"),
            new Category(2, "Fantasy"),
            new Category(3, "Mystery"),
            new Category(4, "Romance"),
            new Category(5, "Horror"),
            new Category(6, "Non-Fiction"),
            new Category(7, "Historical Fiction"),
            new Category(8, "Thriller"),
            new Category(9, "Biography"),
            new Category(10, "Self-Help")
        };

        public static List<Book> Books = new List<Book>()
        {
            new Book { ID = 1, NAME = "Dune", PRICE = 412, ON_SALE = 1,DISCOUNT = 10, CATEGORY_ID = 1 },
            new Book { ID = 2, NAME = "The Hobbit", PRICE = 310, ON_SALE = 1,DISCOUNT = 0, CATEGORY_ID = 2 },
            new Book { ID = 3, NAME = "Sherlock Holmes", PRICE = 221, ON_SALE = 0,DISCOUNT = 50, CATEGORY_ID = 3 }
        };

        public static List<Author> Authors = new List<Author>()
        {
            new Author { ID = 1, NAME = "Frank Herbert", BIOGRAPHY = "This is the biography of author 1" },
            new Author { ID = 2, NAME = "J.R.R. Tolkien", BIOGRAPHY = "This is the biography of author 2" },
            new Author { ID = 3, NAME = "Arthur Conan Doyle", BIOGRAPHY = "This is the biography of author 3" }
        };

        public static List<Compose> Composes = new List<Compose>()
        {
            new Compose { ID = 1, AUTHOR_ID = 1,  BOOK_ID = 1},
            new Compose { ID = 2, AUTHOR_ID = 2 , BOOK_ID = 2},
            new Compose { ID = 3, AUTHOR_ID = 3, BOOK_ID = 3 }
        };


        public static void Initialize()
        {
            // This method can be used to initialize or reset the in-memory database if needed.
            // For now, it does nothing as we are using static lists.
            Categories.Clear();
            Books.Clear();
            Authors.Clear();
            Composes.Clear();
            Categories.AddRange(new List<Category>()
            {
                new Category(1, "Science Fiction"),
                new Category(2, "Fantasy"),
                new Category(3, "Mystery"),
                new Category(4, "Romance"),
                new Category(5, "Horror"),
                new Category(6, "Non-Fiction"),
                new Category(7, "Historical Fiction"),
                new Category(8, "Thriller"),
                new Category(9, "Biography"),
                new Category(10, "Self-Help")
            });
            Books.AddRange(new List<Book>()
            {
                new Book { ID = 1, NAME = "Dune", PRICE = 412, ON_SALE = 1,DISCOUNT = 10, CATEGORY_ID = 1 },
                new Book { ID = 2, NAME = "The Hobbit", PRICE = 310, ON_SALE = 1,DISCOUNT = 0, CATEGORY_ID = 2 },
                new Book { ID = 3, NAME = "Sherlock Holmes", PRICE = 221, ON_SALE = 0,DISCOUNT = 50, CATEGORY_ID = 3 }
            });
            Authors.AddRange(new List<Author>()
            {
                new Author { ID = 1, NAME = "Frank Herbert", BIOGRAPHY = "This is the biography of author 1" },
                new Author { ID = 2, NAME = "J.R.R. Tolkien", BIOGRAPHY = "This is the biography of author 2" },
                new Author { ID = 3, NAME = "Arthur Conan Doyle", BIOGRAPHY = "This is the biography of author 3" }
            });
            Composes.AddRange(new List<Compose>()
            {
                new Compose { ID = 1, AUTHOR_ID = 1,  BOOK_ID = 1},
                new Compose { ID = 2, AUTHOR_ID = 2 , BOOK_ID = 2},
                new Compose { ID = 3, AUTHOR_ID = 3, BOOK_ID = 3 }
            });
        }
        }
    }
