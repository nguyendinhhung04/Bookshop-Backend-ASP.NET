using Oracle.ManagedDataAccess.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bookshop.Models.DAO
{

    public class BookDAO : IDAO<Book>
    {
        private readonly DBConnection connection;

        public BookDAO(DBConnection connection)
        {
            this.connection = connection;
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
                        Console.WriteLine(reader["TITLE"].GetType());
                        Console.WriteLine(reader["NUMBER_OF_PAGE"].GetType());
                        Console.WriteLine(reader["ONSALE"].GetType());
                        books.Add(new Book(
                            Convert.ToInt32(reader["ID"]),
                            reader["TITLE"].ToString(),
                            Convert.ToInt32(reader["NUMBER_OF_PAGE"]),
                            Convert.ToInt32(reader["ONSALE"])
                        ));

                        Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["TITLE"]}, On Sale: {reader["ONSALE"]}");
                    }
                    this.connection.con.Close();
                    return books;
                }
            }
        }
    }
}
