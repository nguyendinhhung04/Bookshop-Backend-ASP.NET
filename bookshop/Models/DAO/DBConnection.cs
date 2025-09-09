using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace bookshop.Models.DAO
{
    public class DBConnection
    {

        //Prerequisite: This app assumes the user has already been created with the
        // necessary privileges
        //Set the demo user id, such as DEMODOTNET and password
        public static string user = "VIETINCAP_CODE";
        public static string pwd = "VIETINCAP_CODE";

        //Set the net service name, Easy Connect, or connect descriptor of the pluggable DB, 
        // such as "localhost/FREEPDB1" for Oracle Database 23ai Free
        public static string db = "\"192.168.2.35:1521/oradb\"";

        public OracleConnection con;

        public DBConnection()
        {
            string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";";
            Console.WriteLine(conStringUser);
            con = new OracleConnection(conStringUser);
            con.Open();
            Console.WriteLine("Successfully connected to Oracle Database as " + user);
            Console.WriteLine();
        }
    }
}
