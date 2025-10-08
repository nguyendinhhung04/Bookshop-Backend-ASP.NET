using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.DataAccessLayer.Models
{
    [Table("BOOKSHOP_BOOK")]
    public class Book
    {


        public Book() { }

        public Book(int? iD, string nAME, float pRICE)
        {
            ID = iD;
            NAME = nAME;
            PRICE = pRICE;
        }

        public Book(int? iD, 
            string nAME, 
            int oN_SALE, 
            float pRICE, 
            float dISCOUNT, 
            string dESCRIPTION, 
            string cOVER_URL, 
            int cATEGORY_ID, 
            DateTime pUBLISH_DATE)
        {
            ID = iD;
            NAME = nAME;
            ON_SALE = oN_SALE;
            PRICE = pRICE;
            DISCOUNT = dISCOUNT;
            DESCRIPTION = dESCRIPTION;
            COVER_URL = cOVER_URL;
            CATEGORY_ID = cATEGORY_ID;
            PUBLISH_DATE = pUBLISH_DATE;
        }


        public int? ID { get;  set; } // Primary key property
        public String NAME { get;  set; }
        public int ON_SALE { get;  set; }
        public float PRICE { get; set; }
        public float DISCOUNT { get; set; }
        public String DESCRIPTION { get; set; }
        public String COVER_URL { get; set; }

        [ForeignKey("Category")]
        public int CATEGORY_ID { get; set; }
        public DateTime PUBLISH_DATE { get; set; }



        public override string? ToString()
        {
            return base.ToString();
        }


    }

}
