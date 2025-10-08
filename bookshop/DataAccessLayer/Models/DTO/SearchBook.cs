namespace bookshop.DataAccessLayer.Models.DTO
{
    public class SearchBook
    {
        public int id { get; set; }
        public String name { get; set; }
        public int category_Id { get; set; }
        public String author_Name { get; set; }
        public int on_Sale { get; set; }
        public float min_Price { get; set; }
        public float max_Price { get; set; }
        public float discount { get; set; }

        public SearchBook() 
        {
        }

        public SearchBook(int id, string name, int category_Id, string author_Name, int on_Sale, float min_Price, float max_Price, float discount)
        {
            this.id = id;
            this.name = name;
            this.category_Id = category_Id;
            this.author_Name = author_Name;
            this.on_Sale = on_Sale;
            this.min_Price = min_Price;
            this.max_Price = max_Price;
            this.discount = discount;
        }
    }
}
