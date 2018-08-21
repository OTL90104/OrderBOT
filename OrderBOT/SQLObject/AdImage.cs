using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.SQLObject
{
    public class AdImage
    {
        public string AdName { get; set; }
        public string AdUri { get; set; }

        internal List<string> SelectAllAdUri()
        {
            List<string> list = new List<string>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectAllAdUricmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string imageUri = reader.GetString(0);

                            list.Add(imageUri);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectAllAdUricmd()
        {
            return @"
SELECT 
      [AdUri]
  FROM [Linebot].[dbo].[AdImageUriTable]
";
        }
    }



}