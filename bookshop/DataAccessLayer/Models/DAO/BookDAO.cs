using bookshop.DataAccessLayer.Models;
using Dapper;
using NuGet.Protocol.Plugins;
using Oracle.ManagedDataAccess.Client;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bookshop.DataAccessLayer.Models.DAO
{

    public class BookDAO : ObjectDAO<Book>
    {
        public BookDAO(DBConnection connection) : base(connection)
        {

        }

        public void AddBook(Book book)
        {
            using (var conn = connection.con)
            {
                String cmd = "INSERT INTO BOOK ";
                String attributes = "";
                String values = "";

                foreach (PropertyInfo i in book.GetType().GetProperties())
                {
                    attributes += i.Name + ","; //Get the name of attribute
                    values += i.GetValue(book) + ","; //Get value of the attribute in book
                }
                if (attributes.Length > 0)
                {
                    attributes = attributes.Substring(0, attributes.Length - 2);
                }
                if (values.Length > 0)
                {
                    values = values.Substring(0, values.Length - 2);
                }

                cmd = cmd + "(" + attributes + ")" + "VALUES" + "(" + values + ")";
                Console.WriteLine(cmd);
            }
        }
    }
}
