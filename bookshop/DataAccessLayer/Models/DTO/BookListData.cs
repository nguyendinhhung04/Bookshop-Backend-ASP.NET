namespace bookshop.DataAccessLayer.Models.DTO
{
    public class BookListData
    {
        public BookListData(int ID, string NAME, string CATEGORY, int ON_SALE, float PRICE, float DISCOUNT)
        {
            this.ID = ID;
            this.NAME = NAME;
            this.CATEGORY = CATEGORY;
            this.ON_SALE = ON_SALE;
            this.PRICE = PRICE;
            this.DISCOUNT = DISCOUNT;
        }

        public BookListData() { }


        public int ID { get; set; }
        public String NAME { get; set; }
        public String CATEGORY { get; set; }
        public int ON_SALE { get; set; }
        public float PRICE { get; set; }

        public float DISCOUNT { get; set; }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
