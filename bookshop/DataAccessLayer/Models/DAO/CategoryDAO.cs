using Dapper;

namespace bookshop.DataAccessLayer.Models.DAO
{
    public class CategoryDAO
    {

        private readonly DBConnection connection;

        public CategoryDAO(DBConnection connection)
        {
            this.connection = connection;
        }

        public async Task<List<Category>> GetAll()
        {
            using (var conn = connection.con)
            {
                var cmd = "SELECT * FROM BOOKSHOP_CATEGORY";
                var result = new List<Category>();
                try
                {
                    result = (await connection.con.QueryAsync<Category>(cmd)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not get all category");
                }
                return result;
            }
        }

        public async Task<Category> GetOneCategory(int id)
        {
            using (var conn = connection.con)
            {
                var cmd = "SELECT * FROM BOOKSHOP_CATEGORY WHERE ID = :id";
      
                var result = new Category();
                try
                {
                    result = (await connection.con.QueryAsync<Category>(cmd, new {id = id})).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not find category");
                }
                return result;
            }
        }

    }
}
