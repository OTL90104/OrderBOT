using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class ShopItem
    {
        public string ShopID { get; set; }

        public string shopItem { get; private set; }
        public double ShopItemPrice { get; private set; }

        public ShopItem()
        {
        }

        public ShopItem(string ShopID)
        {
            this.ShopID = ShopID;
        }

        internal List<ShopItem> SelectByMyShopID()
        {
            List<ShopItem> list = new List<ShopItem>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByMyShopIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopItem shopInfo = new ShopItem();
                            shopInfo.ShopID = reader.GetString(0);
                            shopInfo.shopItem = reader.GetString(1);
                            shopInfo.ShopItemPrice = reader.GetDouble(2);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        internal List<ShopItem> SelectByClubShopID()
        {
            List<ShopItem> list = new List<ShopItem>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByClubShopIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopItem shopInfo = new ShopItem();
                            shopInfo.ShopID = reader.GetString(0);
                            shopInfo.shopItem = reader.GetString(1);
                            shopInfo.ShopItemPrice = reader.GetDouble(2);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        internal List<ShopItem> SelectByBossShopID()
        {
            List<ShopItem> list = new List<ShopItem>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByBossShopIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopItem shopInfo = new ShopItem();
                            shopInfo.ShopID = reader.GetString(0);
                            shopInfo.shopItem = reader.GetString(1);
                            shopInfo.ShopItemPrice = reader.GetDouble(2);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectByMyShopIDcmd()
        {
            return @"
                    SELECT 
                           [MyShopID]
                          ,[MyShopItem]
                          ,[MyShopItemPrice]
                      FROM [dbo].[MyShopItemTable]
                    WHERE MyShopID = @ShopID
                    ";
        }

        private string SelectByBossShopIDcmd()
        {
            return @"
                    SELECT 
                           [BossShopID]
                          ,[BossShopItem]
                          ,[BossShopItemPrice]
                      FROM [dbo].[BossShopItemTable]
                    WHERE BossShopID = @ShopID
                    ";
        }

        private string SelectByClubShopIDcmd()
        {
            return @"
                    SELECT 
                           [ClubShopID]
                          ,[ClubShopItem]
                          ,[ClubShopItemPrice]
                      FROM [dbo].[ClubShopItemTable]
                    WHERE ClubShopID = @ShopID
                    ";
        }
    }
}