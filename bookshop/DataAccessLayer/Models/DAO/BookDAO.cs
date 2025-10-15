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
        private readonly int pageSize = 8;

        public BookDAO(DBConnection connection)
        {
            this.connection = connection;
        }
        public async Task<bool> Add(AddedBook book)
        {
            using (var conn = connection.con)
            {
               
                bool result = true;
                conn.BeginTransaction();
                try
                {
                    var bookParameters = new DynamicParameters();
                    bookParameters.Add("p_name", book.NAME);
                    bookParameters.Add("p_onSale", book.ON_SALE);
                    bookParameters.Add("p_price", book.PRICE);
                    bookParameters.Add("p_discount", book.DISCOUNT);
                    bookParameters.Add("p_description", book.DESCRIPTION);
                    bookParameters.Add("p_coverURL", book.COVER_URL);
                    bookParameters.Add("p_category", book.CATEGORY_ID);
                    bookParameters.Add("p_publishDate", book.PUBLISH_DATE);
                    bookParameters.Add("p_result", dbType: DbType.Int64, direction: ParameterDirection.Output);

                    await conn.ExecuteReaderAsync(
                        "vietincap_code.ADD_BOOK",
                        bookParameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    if (bookParameters.Get<Int64>("p_result") == 0)
                    {
                        Console.WriteLine("Cannot add book");
                        return false;
                    }

                    foreach (var authorId in book.AUTHORS_ID)
                    {
                        var composeParamenters = new DynamicParameters();
                        composeParamenters.Add("p_bookId", bookParameters.Get<Int64>("p_result"));
                        composeParamenters.Add("p_authorId", authorId);
                        composeParamenters.Add("p_result", dbType: DbType.Int64, direction: ParameterDirection.Output);
                        await conn.ExecuteReaderAsync(
                            "vietincap_code.ADD_COMPOSE",
                            composeParamenters,
                            commandType: System.Data.CommandType.StoredProcedure
                        );
                        if (composeParamenters.Get<Int64>("p_result") == 0)
                        {
                            Console.WriteLine("Cannot add compose");
                            return false;
                        }
                    }
                    conn.Commit();
                }
                catch (Exception ex) {
                    conn.Rollback();
                    Console.WriteLine("Cannot add book");
                    result = false;
                }
                return result;
            }

            
        }

        public async Task<List<BookListData>> FindBookByName(string name, int page)
        {
            using (var conn = connection.con)
            {
                var cmd = @"SELECT 
                    b.ID,b.NAME, c.NAME AS CATEGORY, b.ON_SALE, b.PRICE, b.DISCOUNT 
                    FROM BOOKSHOP_BOOK b INNER JOIN BOOKSHOP_CATEGORY c ON b.CATEGORY_ID = c.ID 
                    WHERE b.NAME LIKE  :name 
                    ORDER BY b.ID ASC 
                    OFFSET :offset ROWS FETCH NEXT 8 ROWS ONLY 
                    ";
                List<BookListData> result = null;
                try
                {
                    result = (await connection.con.QueryAsync<BookListData>(cmd, new {name = "%" + name + "%", offset = (page-1)* pageSize })).ToList();
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

                String cmd = "SELECT " +
                    "b.ID, b.NAME, b.ON_SALE, b.PRICE, b.DISCOUNT, b.DESCRIPTION, b.COVER_URL, " +
                    "c.NAME AS CATEGORY, " +
                    "b.PUBLISH_DATE, " +
                    "LISTAGG(a.NAME, ', ') WITHIN GROUP (ORDER BY a.NAME) AS AUTHORS " +
                    "FROM BOOKSHOP_BOOK b " +
                    "INNER JOIN BOOKSHOP_CATEGORY c ON b.CATEGORY_ID = c.ID " +
                    "LEFT JOIN BOOKSHOP_COMPOSE comp ON b.ID = comp.BOOK_ID " +
                    "LEFT JOIN BOOKSHOP_AUTHOR a ON comp.AUTHOR_ID = a.ID " +
                    "WHERE b.ID = :id " +
                    "GROUP BY b.ID, b.NAME, b.ON_SALE, b.PRICE, b.DISCOUNT, b.DESCRIPTION, b.COVER_URL, c.NAME, b.PUBLISH_DATE";
                //thừa dấu chấm phẩy ở cuối câu lệnh SQL

                BookDetail result = null;
                try
                {
                    result = (await connection.con.QueryAsync<BookDetail>(cmd, new { id = id })).FirstOrDefault();
                    //await conn.ExecuteAsync(cmd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not Query book with id = " + id);
                }
                return result;
            }
        }

        public async Task<bool> UpdateBook(UpdateBook updateBook)
        {
            using (var conn = connection.con)
            {
                String cmd = "UPDATE BOOKSHOP_BOOK SET " +
                    "NAME = :name, DESCRIPTION = :des, ON_SALE = :onsale, PRICE = :price, DISCOUNT = :discount " +
                    "WHERE ID = :id";
                bool result = false;
                conn.BeginTransaction();
                try
                {
                    var parameters = new
                    {
                        id = updateBook.ID,
                        name = updateBook.NAME,
                        des = updateBook.DESCRIPTION,
                        onsale = updateBook.ON_SALE,
                        price = updateBook.PRICE,
                        discount = updateBook.DISCOUNT
                    };

                    int rowsAffected = await conn.ExecuteAsync(cmd, parameters);
                    conn.Commit();
                    return rowsAffected > 0; // true nếu có ít nhất 1 row được update

                }
                catch(Exception e)
                {
                    Console.WriteLine("Can not update book");
                    conn.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var conn = connection.con)
            {
                String cmd = "DELETE FROM BOOKSHOP_BOOK WHERE ID = :id";
                bool result = false;

                conn.BeginTransaction();
                try
                {
                    int rowsAffected = await conn.ExecuteAsync(cmd, new { id = id });
                    conn.Commit();
                    return rowsAffected > 0; // true nếu có ít nhất 1 row bị xóa
                }
                catch (Exception e)
                {
                    Console.WriteLine("Can not delete book");
                    conn.Rollback();
                    return false;
                }
            }
        }

        public async Task<List<BookListData>> CustomSearchBook(SearchBook searchBook, int page)
        {
            
            using(var conn = connection.con)
            {
                var cmd = @"
                            SELECT DISTINCT
                                b.ID,
                                b.NAME,
                                c.NAME AS CATEGORY,
                                b.ON_SALE,
                                b.PRICE,
                                b.DISCOUNT 
                            FROM BOOKSHOP_BOOK b 
                            INNER JOIN BOOKSHOP_CATEGORY c ON b.CATEGORY_ID = c.ID 
                            LEFT JOIN BOOKSHOP_COMPOSE comp ON b.ID = comp.BOOK_ID
                            LEFT JOIN BOOKSHOP_AUTHOR a ON comp.AUTHOR_ID = a.ID
                            WHERE ";
                List<BookListData> result = null;

                List<String> conditions = new List<string>();
                conditions.Add(" 1=1 ");
                if (searchBook==null)
                {
                    return null;
                }
                if (searchBook.min_Price != null)
                {
                    conditions.Add("b.PRICE BETWEEN " + searchBook.min_Price + " AND " + searchBook.max_Price);
                }
                if (searchBook.id != -1)
                {
                    conditions.Add("b.ID = " + searchBook.id);
                }
                if (searchBook.name != null && searchBook.name.Trim() != "")
                {
                    conditions.Add("b.NAME LIKE '%" + searchBook.name.Trim() + "%'");
                }
                if (searchBook.author_Name != null && searchBook.author_Name.Trim() != "")
                {
                    conditions.Add("a.NAME LIKE '%" + searchBook.author_Name.Trim() + "%'");
                }
                if (searchBook.category_Id != -1 )
                {
                    conditions.Add("b.CATEGORY_ID = " + searchBook.category_Id);
                }
                if(searchBook.on_Sale != -1)
                {
                    conditions.Add("b.ON_SALE = " + searchBook.on_Sale);
                }
                if (searchBook.discount != 0)
                {
                    conditions.Add("b.DISCOUNT >= " + searchBook.discount);
                }

                cmd += String.Join(" AND ", conditions);
                cmd += " ORDER BY b.ID ASC ";
                cmd += "OFFSET :offset ROWS FETCH NEXT 8 ROWS ONLY";

                Console.WriteLine("SQL command: " + cmd);
                try
                {
                    result = (await connection.con.QueryAsync<BookListData>(cmd, new {offset = (page - 1) * pageSize })).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not Query book with custom search");
                    return null;
                }


            }
        }

    }

    
}
