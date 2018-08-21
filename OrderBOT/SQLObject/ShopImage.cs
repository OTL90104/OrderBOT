using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.SQLObject
{
    public class ShopImage
    {
        public string ShopID { get; set; }
        public string ImageName { get; set; }
        public string ImageUri { get; set; }
        public string ImageType { get; set; }
        public ShopImage(string ShopID)
        {
            this.ShopID = ShopID;
        }

        internal List<string> SelectImageByShopID()
        {
            List<string> list = new List<string>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectImageByShopIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);

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

        private string SelectImageByShopIDcmd()
        {
            return @"

SELECT 
      [ImageUri]

  FROM [Linebot].[dbo].[ShopImageTable]

  WHERE [ShopID] = @ShopID;
";
        }
    }
}