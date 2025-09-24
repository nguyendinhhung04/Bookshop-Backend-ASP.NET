namespace bookshop.DataAccessLayer.Models.DAO
{
    public static class TableNameDictionary
    {
        public static Dictionary<string, string> TableNames = new Dictionary<string, string>
        {
            { "Book", "BOOK" },
            { "Author", "AUTHOR" },
            { "Category", "CATEGORY" },
            { "Compose", "COMPOSE" }
        };
    }
}
