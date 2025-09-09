using Oracle.ManagedDataAccess.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bookshop.Models.DAO
{
    //public class BookDAO : DBConnection
    //{

    //    public List<Book> GetAll()
    //    {

    //        string query = "SELECT * FROM BOOK";

    //        //Create a OracleCommand 
    //        using (OracleCommand cmd = new OracleCommand(query, con))
    //        {
    //            //Execute cmd
    //            using (OracleDataReader reader = cmd.ExecuteReader())
    //            {
    //                List<Book> books = new List<Book>();
    //                //Use While to read data
    //                while (reader.Read())
    //                {
    //                    books.Add(new Book(
    //                        reader["TITLE"].ToString(),
    //                        Convert.ToInt32(reader["NUMBER_OF_PAGE"]),
    //                        Convert.ToInt32(reader["ONSALE"])
    //                    ));

    //                    Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["TITLE"]}, On Sale: {reader["ONSALE"]}");
    //                }
    //                this.con.Close();
    //                return books;
    //            }
    //        }
    //    }

    //}

    public class BookDAO : IDAO<Book>
    {
        private readonly DBConnection connection;
        private readonly TempData data;

        public BookDAO(DBConnection connection, TempData data)
        {
            this.connection = connection;
            this.data = data;
        }

        public List<Book> GetAll()
        {

            string query = "SELECT * FROM BOOK";

            //Create a OracleCommand 
            using (OracleCommand cmd = new OracleCommand(query, this.connection.con))
            {
                //Execute cmd
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    List<Book> books = new List<Book>();
                    //Use While to read data
                    while (reader.Read())
                    {
                        //Console.WriteLine(reader["TITLE"].GetType());
                        //Console.WriteLine(reader["NUMBER_OF_PAGE"].GetType());
                        //Console.WriteLine(reader["ONSALE"].GetType());
                        //books.Add(new Book(
                        //    reader["TITLE"].ToString(),
                        //    Convert.ToInt32(reader["NUMBER_OF_PAGE"]),
                        //    Convert.ToInt32(reader["ONSALE"])
                        //));

                        Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["TITLE"]}, On Sale: {reader["ONSALE"]}");
                    }
                    this.connection.con.Close();
                    return books;
                }
            }
        }

        public List<Book> GetTempData()
        {
            return this.data.books;
        }

    }
}
