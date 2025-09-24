using bookshop.DataAccessLayer.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<bool> AddBook(Book book)
        {
            using (var conn = connection.con)
            {
                //Get next value in sequence
                if(book.ID == null)
                {
                    String nextVal = conn.Query("SELECT bookshop_book_seq.NEXTVAL FROM dual;").ToString();
                    try{
                        book.ID = Convert.ToInt32(nextVal);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Can not convert Sequence Value to Int");
                    }
                }

                String cmd = "INSERT INTO BOOK ";
                String attributes = "";
                String values = "";

                //Generate query String
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

                bool result = true;
                try
                {
                    await conn.QueryAsync<Book>(cmd);
                }
                catch (Exception e) {
                    Console.WriteLine("Can not Save book");
                    result = false;
                }
                return result;

            }
        }
    }
}
