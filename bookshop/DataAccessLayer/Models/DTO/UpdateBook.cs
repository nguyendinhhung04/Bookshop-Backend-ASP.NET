namespace bookshop.DataAccessLayer.Models.DTO
{
    public class UpdateBook
    {
        public UpdateBook(int iD, string nAME, string dESCRIPTION, int oN_SALE, float pRICE, float dISCOUNT)
        {
            ID = iD;
            NAME = nAME;
            DESCRIPTION = dESCRIPTION;
            ON_SALE = oN_SALE;
            PRICE = pRICE;
            DISCOUNT = dISCOUNT;
        }

        public UpdateBook() { }

        public int ID { get; set; }
        public String NAME { get; set; }
        public String DESCRIPTION { get; set; }
        public int ON_SALE { get; set; }
        public float PRICE { get; set; }
        public float DISCOUNT { get; set; }
    }
}
