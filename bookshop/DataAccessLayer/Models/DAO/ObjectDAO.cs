using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace bookshop.DataAccessLayer.Models.DAO
{
    public class ObjectDAO<T> : IDAO<T> where T : class
    {
        protected readonly DBConnection connection;

        public ObjectDAO(DBConnection connection)
        {
            this.connection = connection;
        }

        public List<T> GetAll()
        {
            using (var conn = connection.con)
            {
                //CommandDefinition cmd = new CommandDefinition(
                //    commandText: "SELECT * FROM @tableName",
                //    parameters: new
                //    {
                //        //Get the table name in the dictionary corresponding to the object
                //        tableName = TableNameDictionary.TableNames[typeof(T).Name.ToString()],
                //    },
                //    commandType: CommandType.Text,
                //    commandTimeout: 30
                //);
                var cmd = "SELECT * FROM " + TableNameDictionary.TableNames[typeof(T).Name.ToString()];
                var result =  connection.con.Query<T>(cmd).ToList();
                return result;
            }
        }

        
    }
}
