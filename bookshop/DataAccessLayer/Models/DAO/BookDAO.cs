using bookshop.DataAccessLayer.Models;
using bookshop.DataAccessLayer.Models.DTO;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bookshop.DataAccessLayer.Models.DAO
{

    public class BookDAO : IDAO
    {
        private readonly DBConnection connection;

        public BookDAO(DBConnection connection)
        {
            this.connection = connection;
        }
        //public async Task<bool> AddBook(Book book)
        //{
        //    using (var conn = connection.con)
        //    {
        //        //Get next value in sequence
        //        if(book.ID == null)
        //        {
        //            String nextVal = conn.Query("SELECT bookshop_book_seq.NEXTVAL FROM dual;").ToString();
        //            try{
        //                book.ID = Convert.ToInt32(nextVal);
        //            }
        //            catch(Exception e)
        //            {
        //                Console.WriteLine("Can not convert Sequence Value to Int");
        //            }
        //        }

        //        String cmd = "INSERT INTO BOOK ";
        //        String attributes = "";
        //        String values = "";

        //        //Generate query String
        //        foreach (PropertyInfo i in book.GetType().GetProperties())
        //        {
        //            attributes += i.Name + ","; //Get the name of attribute
        //            values += i.GetValue(book) + ","; //Get value of the attribute in book
        //        }
        //        if (attributes.Length > 0)
        //        {
        //            attributes = attributes.Substring(0, attributes.Length - 2);
        //        }
        //        if (values.Length > 0)
        //        {
        //            values = values.Substring(0, values.Length - 2);
        //        }

        //        cmd = cmd + "(" + attributes + ")" + "VALUES" + "(" + values + ")";
        //        Console.WriteLine(cmd);

        //        bool result = true;
        //        try
        //        {
        //            await conn.QueryAsync<Book>(cmd);
        //        }
        //        catch (Exception e) {
        //            Console.WriteLine("Can not Save book");
        //            result = false;
        //        }
        //        return result;

        //    }
        //}

        public async Task<bool> Add(Book book)
        {
            using (var conn = connection.con)
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_name", book.NAME);
                parameters.Add("p_numPage", book.NUMBER_OF_PAGE);
                parameters.Add("p_onSale", book.ON_SALE);
                parameters.Add("p_price", book.PRICE);
                parameters.Add("p_discount", book.DISCOUNT);
                parameters.Add("p_description", book.DESCRIPTION);
                parameters.Add("p_coverURL", book.COVER_URL);
                parameters.Add("p_category", book.CATEGORY_ID);
                parameters.Add("p_publishDate", book.PUBLISH_DATE);
                parameters.Add("p_result", dbType: DbType.Int64, direction: ParameterDirection.Output);

                bool result = true;
                try
                {
                    await conn.ExecuteReaderAsync(
                        "vietincap_code.ADD_BOOK",
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );
                }
                catch (Exception ex) {
                    Console.WriteLine("Cannot add book");
                    result = false;
                }
                return result;
            }

            
        }

        public async Task<List<BookListData>> GetAll()
        {
            using (var conn = connection.con)
            {
                var cmd = "SELECT " +
                    "b.ID,b.NAME, c.NAME AS CATEGORY, b.ON_SALE, b.PRICE" +
                    " FROM BOOKSHOP_BOOK b INNER JOIN BOOKSHOP_CATEGORY c ON b.CATEGORY_ID = c.ID";
                List<BookListData> result = null;
                try
                {
                    result = (await connection.con.QueryAsync<BookListData>(cmd)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not Query all book");
                }
                return result;
            }
        }

        public async Task<BookDetail> GetOneBook(int id)
        {
            using (var conn = connection.con)
            {
                var cmd = "SELECT " +
                    "b.ID,b.NAME, b.NUMBER_OF_PAGE, b.ON_SALE, b.PRICE, b.DISCOUNT, b.DESCRIPTION, b.COVER_URL, c.NAME AS CATEGORY, b.PUBLISH_DATE" +
                    " FROM BOOKSHOP_BOOK b INNER JOIN BOOKSHOP_CATEGORY c ON b.CATEGORY_ID = c.ID" +
                    " WHERE b.ID = :id";
                BookDetail result = null;
                try
                {
                    result = (await connection.con.QueryAsync<BookDetail>(cmd, new { id = id })).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not Query book with id = " + id);
                }
                return result;
            }
        }
    }

    
}
