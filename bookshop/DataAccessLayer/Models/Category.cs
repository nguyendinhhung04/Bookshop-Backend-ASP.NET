using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.DataAccessLayer.Models
{
    [Table("CATEGORY")]
    public class Category
    {
        public int ID { get; set; }
        public String NAME { get; set; }

        public Category() { }

        public Category(int iD, String nAME)
        {
            ID = iD;
            NAME = nAME;
        }



        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
