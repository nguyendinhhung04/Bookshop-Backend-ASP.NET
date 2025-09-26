using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.DataAccessLayer.Models.DTO
{
    public class BookDetail
    {

        public int? ID { get; set; } // Primary key property
        public String NAME { get; set; }
        public int NUMBER_OF_PAGE { get; set; }
        public int ON_SALE { get; set; }
        public float PRICE { get; set; }
        public float DISCOUNT { get; set; }
        public String DESCRIPTION { get; set; }
        public String COVER_URL { get; set; }

        [ForeignKey("Category")]
        public String CATEGORY { get; set; }
        public DateTime PUBLISH_DATE { get; set; }

    }
}
