using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.DataAccessLayer.Models.DTO
{
    public class AddedBook
    {
        public AddedBook()
        {
        }

        public String NAME { get; set; }
        public int ON_SALE { get; set; }
        public float PRICE { get; set; }
        public float DISCOUNT { get; set; }
        public String DESCRIPTION { get; set; }
        public String COVER_URL { get; set; }
        public int CATEGORY_ID { get; set; }
        public DateTime PUBLISH_DATE { get; set; }
        public List<int> AUTHORS_ID { get; set; }
        }
}
