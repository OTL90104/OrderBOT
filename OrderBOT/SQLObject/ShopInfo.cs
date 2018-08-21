using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    /// <summary>
    /// ClubShopTable / MyShopTable 溝通SQL所使用的物件
    /// </summary>
    public class ShopInfo
    {
        public string ClubIDorUserIDorBossID;
        public string ShopID { get; set; }
        public string ShopName { get; set; }
        public string ShopPhone { get; set; }
        public string ShopAddress { get; set; }
        public string ShopItem { get; set; }
        public double ShopItemPrice { get; set; }

        public ShopInfo()
        {

        }

        public ShopInfo(string ClubIDorUserIDorBossID)
        {
            this.ClubIDorUserIDorBossID = ClubIDorUserIDorBossID;
        }

        public List<ShopInfo> MyShopSelectByUserid()
        {
            List<ShopInfo> list = new List<ShopInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(MyShopSelectByUseridcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.ClubIDorUserIDorBossID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ClubIDorUserIDorBossID = reader.GetString(0);
                            shopInfo.ShopID = reader.GetString(1);
                            shopInfo.ShopName = reader.GetString(2);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        private string MyShopSelectByUseridcmd()
        {
            return @"
                    SELECT  [UserID]
                          ,[MyShopID]
                          ,[MyShopName]
                      FROM [Linebot].[dbo].[MyShopTable]
                    WHERE UserID = @UserID;
                    ";
        }


        internal List<ShopInfo> ClubShopSelectShopNameByShopid()
        {
            List<ShopInfo> list = new List<ShopInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(ClubShopSelectByShopidcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ShopName = reader.GetString(0);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        private string ClubShopSelectByShopidcmd()
        {
            return @"
                    SELECT 
                          [ClubShopName]
                      FROM [Linebot].[dbo].[ClubShopTable]
                    WHERE ClubShopID = @ShopID;
                    ";
        }


        internal List<ShopInfo> BossShopSelectShopNameByShopid()
        {
            List<ShopInfo> list = new List<ShopInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(BossShopSelectByShopidcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ShopName = reader.GetString(0);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        private string BossShopSelectByShopidcmd()
        {
            return @"
                    SELECT 
                          [BossShopName]
                      FROM [Linebot].[dbo].[BossShopTable]
                    WHERE BossShopID = @ShopID;
                    ";
        }


        public List<ShopInfo> MyShopSelectShopNameByShopid()
        {
            List<ShopInfo> list = new List<ShopInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(MyShopSelectByShopidcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ShopName = reader.GetString(0);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        private string MyShopSelectByShopidcmd()
        {
            return @"
                    SELECT 
                          [MyShopName]
                      FROM [Linebot].[dbo].[MyShopTable]
                    WHERE MyShopID = @MyShopID;
                    ";
        }


        public List<ShopInfo> ClubShopSelectByClubID()
        {
            List<ShopInfo> list = new List<ShopInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(ClubShopSelectByClubIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubIDorUserIDorBossID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ClubIDorUserIDorBossID = reader.GetString(0);
                            shopInfo.ShopID = reader.GetString(1);
                            shopInfo.ShopName = reader.GetString(2);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        private string ClubShopSelectByClubIDcmd()
        {
            return @"
                    SELECT  [ClubID]
                          ,[ClubShopID]
                          ,[ClubShopName]
                      FROM [Linebot].[dbo].[ClubShopTable]
                    WHERE ClubID = @ClubID;
                    ";
        }


        internal List<ShopInfo> BossShopSelectByUserID()
        {
            List<ShopInfo> list = new List<ShopInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(BossShopSelectByUserIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ClubIDorUserIDorBossID = reader.GetString(0);
                            shopInfo.ShopID = reader.GetString(1);
                            shopInfo.ShopName = reader.GetString(2);
                            list.Add(shopInfo);
                        }
                    }
                }
            }
            return list;
        }

        private string BossShopSelectByUserIDcmd()
        {
            return @"
                    SELECT [BossID]
                          ,[BossShopID]
                          ,[BossShopName]
                      FROM [Linebot].[dbo].[BossShopTable]
                    ";
        }


        internal void SelectMyShopNameByShopID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectMyShopNameByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.ShopName = reader.GetString(0);
                        }
                    }
                }
            }
        }

        private string SelectMyShopNameByShopIDCmd()
        {
            return @"
                    SELECT 
                          [MyShopName]
                      FROM [Linebot].[dbo].[MyShopTable]
                    WHERE MyShopID = @MyShopID;
                    ";
        }


        internal void SelectClubShopNameByShopID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectClubShopNameByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.ShopName = reader.GetString(0);
                        }
                    }
                }
            }
        }

        private string SelectClubShopNameByShopIDCmd()
        {
            return @"
                    SELECT 
                          [ClubShopName]
                      FROM [Linebot].[dbo].[ClubShopTable]
                    WHERE ClubShopID = @ClubShopID;
                    ";
        }



        #region 刪除商店(MyShop/ClubShop)
        internal int DeleteMyShopByUserIDandShopID(string ShopID)
        {
            this.ShopID = ShopID;

            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteMyShopByUserIDandShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.ClubIDorUserIDorBossID);
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string DeleteMyShopByUserIDandShopIDCmd()
        {
            return @"
                    DELETE FROM [dbo].[MyShopTable]
                          WHERE UserID = @UserID and MyShopID = @MyShopID;
                    
                    DELETE FROM [dbo].[MyShopDetailTable]
                          WHERE MyShopID = @MyShopID;
                    
                    DELETE FROM [dbo].[MyShopItemTable]
                          WHERE MyShopID = @MyShopID;
                    ";
        }


        internal int DeleteClubShopByClubIDandShopID(string ShopID)
        {
            this.ShopID = ShopID;

            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteClubShopByClubIDandShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubIDorUserIDorBossID);
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string DeleteClubShopByClubIDandShopIDCmd()
        {
            return @"
                    DELETE FROM [dbo].[ClubShopTable]
                          WHERE ClubID = @ClubID and ClubShopID = @ClubShopID;
                    
                    DELETE FROM [dbo].[ClubShopDetailTable]
                          WHERE ClubShopID = @ClubShopID;
                    
                    DELETE FROM [dbo].[ClubShopItemTable]
                          WHERE ClubShopID = @ClubShopID;
                    ";
        }
        #endregion


        #region 修改商店名稱(MyShop/ClubShop)
        internal int UpdateMyShopNameByShopID(string ShopID)
        {
            this.ShopID = ShopID;

            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateMyShopNameByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@MyShopName", this.ShopName);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateMyShopNameByShopIDCmd()
        {
            return @"
                    UPDATE [dbo].[MyShopTable]
                       SET 
                          [MyShopName] = @MyShopName
                     WHERE MyShopID = @MyShopID;

                    UPDATE [dbo].[MyShopDetailTable]
                       SET 
                          [MyShopName] = @MyShopName     
                     WHERE MyShopID = @MyShopID
                    ";
        }


        internal int UpdateClubShopNameByShopID(string ShopID)
        {
            this.ShopID = ShopID;

            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateClubShopNameByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@ClubShopName", this.ShopName);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateClubShopNameByShopIDCmd()
        {
            return @"
                    UPDATE [dbo].[ClubShopTable]
                       SET 
                          [ClubShopName] = @ClubShopName
                     WHERE ClubShopID = @ClubShopID;

                    UPDATE [dbo].[ClubShopDetailTable]
                       SET 
                          [ClubShopName] = @ClubShopName     
                     WHERE ClubShopID = @ClubShopID
                    ";
        }
        #endregion


        #region 修改商店電話(MyShop/ClubShop)
        internal int UpdateMyShopPhoneByShopID(string ShopID)
        {
            this.ShopID = ShopID;

            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateMyShopPhoneByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@MyShopPhone", this.ShopPhone);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateMyShopPhoneByShopIDCmd()
        {
            return @"
                     UPDATE [dbo].[MyShopDetailTable]
                       SET 
                          [MyShopPhone] = @MyShopPhone     
                     WHERE MyShopID = @MyShopID
                    ";
        }


        internal int UpdateClubShopPhoneByShopID(string ShopID)
        {
            this.ShopID = ShopID;

            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateClubShopPhoneByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@ClubShopPhone", this.ShopPhone);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateClubShopPhoneByShopIDCmd()
        {
            return @"
                     UPDATE [dbo].[ClubShopDetailTable]
                       SET 
                          [ClubShopPhone] = @ClubShopPhone     
                     WHERE ClubShopID = @ClubShopID
                    ";
        }
        #endregion


        #region 修改商店地址(MyShop/ClubShop)
        internal int UpdateMyShopAddressByShopID(string ShopID)
        {
            this.ShopID = ShopID;

            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateMyShopAddressByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@MyShopAddress", this.ShopAddress);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateMyShopAddressByShopIDCmd()
        {
            return @"
                     UPDATE [dbo].[MyShopDetailTable]
                       SET 
                          [MyShopAddress] = @MyShopAddress     
                     WHERE MyShopID = @MyShopID
                    ";
        }


        internal int UpdateClubShopAddressByShopID(string ShopID)
        {
            this.ShopID = ShopID;

            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateClubShopAddressByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@ClubShopAddress", this.ShopAddress);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateClubShopAddressByShopIDCmd()
        {
            return @"
                     UPDATE [dbo].[ClubShopDetailTable]
                       SET 
                          [ClubShopAddress] = @ClubShopAddress     
                     WHERE ClubShopID = @ClubShopID
                    ";
        }
        #endregion


        internal List<ShopInfo> SelectItemByMyShopID()
        {
            List<ShopInfo> shopInfos = new List<ShopInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectItemByMyShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ShopItem = reader.GetString(0);
                            shopInfo.ShopItemPrice = reader.GetDouble(1);
                            shopInfos.Add(shopInfo);
                        }
                    }
                }
            }
            return shopInfos;
        }

        private string SelectItemByMyShopIDCmd()
        {
            return @"
                    SELECT 
                           [MyShopItem]
                          ,[MyShopItemPrice]
                      FROM [dbo].[MyShopItemTable]
                      WHERE MyShopID = @MyShopID
                    ";
        }



        internal List<ShopInfo> SelectItemByClubShopID()
        {
            List<ShopInfo> shopInfos = new List<ShopInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectItemByClubShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopInfo shopInfo = new ShopInfo();
                            shopInfo.ShopItem = reader.GetString(0);
                            shopInfo.ShopItemPrice = reader.GetDouble(1);
                            shopInfos.Add(shopInfo);
                        }
                    }
                }
            }
            return shopInfos;
        }

        private string SelectItemByClubShopIDCmd()
        {
            return @"
                    SELECT 
                           [ClubShopItem]
                          ,[ClubShopItemPrice]
                      FROM [dbo].[ClubShopItemTable]
                      WHERE ClubShopID = @ClubShopID
                    ";
        }

        internal void SelectBossShopNameByShopID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectBossShopNameByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BossShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.ShopName = reader.GetString(0);
                        }
                    }
                }
            }
        }

        private string SelectBossShopNameByShopIDCmd()
        {
            return @"
SELECT [BossShopName]
  FROM [Linebot].[dbo].[BossShopTable]
                    WHERE [BossShopID] = @BossShopID;
                    ";
        }
        internal void SelectBossIDByShopID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectBossIDByShopIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BossShopID", this.ShopID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.ClubIDorUserIDorBossID = reader.GetString(0);
                        }
                    }
                }
            }
        }

        private string SelectBossIDByShopIDCmd()
        {
            return @"
SELECT  [BossID]

  FROM [Linebot].[dbo].[BossShopTable]
  WHERE [BossShopID] = @BossShopID
";
        }
    }
}