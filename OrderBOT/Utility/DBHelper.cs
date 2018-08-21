using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class DBHelper
    {

        //   public static string connectionString = ConfigurationManager
        //.ConnectionStrings["Data Source=STAN-PC;Initial Catalog=Linebot;Integrated Security=True"]
        //.ConnectionString;

       // public static string connectionString = "Data Source=STAN-PC;Initial Catalog=Linebot;Integrated Security=True";
        public static string connectionString = "Data Source=(local);Initial Catalog=Linebot;Integrated Security=True";
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                cmd.Connection = connection;
                try
                {
                    count = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    connection.Close();
                }
            }
            return count;
        }
    }
}