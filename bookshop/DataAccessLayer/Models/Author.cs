using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.DataAccessLayer.Models
{
    [Table("AUTHOR")]
    public class Author
    {

        public Author() { }
        public Author(int iD, string nAME)
        {
            ID = iD;
            NAME = nAME;
        }
        public int ID { get; set; } // Primary key property
        public String NAME { get; set; }
        public String BIOGRAPHY { get; set; }
    }
}
