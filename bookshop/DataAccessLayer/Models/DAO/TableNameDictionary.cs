namespace bookshop.DataAccessLayer.Models.DAO
{
    public static class TableNameDictionary
    {
        public static Dictionary<string, string> TableNames = new Dictionary<string, string>
        {
            { "Book", "BOOKSHOP_BOOK" },
            { "Author", "BOOKSHOP_AUTHOR" },
            { "Category", "BOOKSHOP_CATEGORY" },
            { "Compose", "BOOKSHOP_COMPOSE" }
        };
    }
}
