using OrderBot.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.SQLObject
{
    public class ShopListTemp
    {
        private string UserID;
        public string ShopID { get; set; }
        public ShopListTemp(string userId)
        {
            this.UserID = userId;
        }

        public ShopListTemp()
        {
        }

        internal void InsertShopIDByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertShopIDByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string InsertShopIDByUserIDCmd()
        {
            return @"
INSERT INTO [dbo].[ShopListTempTable]
           ([UserID]
           ,[ShopID])
     VALUES
           (@UserID
           ,@ShopID)
";
        }

        internal List<ShopListTemp> SelectByUserID()
        {
            List<ShopListTemp> list = new List<ShopListTemp>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByUseridcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopListTemp shopListTemp = new ShopListTemp();
                            shopListTemp.UserID = reader.GetString(0);
                            shopListTemp.ShopID = reader.GetString(1);
                            list.Add(shopListTemp);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectByUseridcmd()
        {
            return @"
SELECT [UserID]
      ,[ShopID]
  FROM [Linebot].[dbo].[ShopListTempTable]
WHERE UserID = @UserID;
";
        }

        internal void DeleteByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeletByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string DeletByUserIDCmd()
        {
            return @"
DELETE FROM [dbo].[ShopListTempTable]
      WHERE [UserID] = UserID;
";
        }
    }
}