using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class ShopTemp
    {
        private readonly string UserID;
        public string ShopName { get; set; }
        public string ShopPhone { get; set; }
        public string ShopAddress { get; set; }
        public string ClubID { get; set; }
        public string ShopID { get; set; }
        public string ShopItem { get; set; }
        public double ShopItemPrice { get; set; }

        public ShopTemp()
        {

        }

        public ShopTemp(string UserID)
        {
            this.UserID = UserID;
            this.ShopName = "No Data";
            this.ShopPhone = "No Data";
            this.ShopAddress = "No Data";
            this.ClubID = "No Data";
            this.ShopItem = "No Data";
            this.ShopItemPrice = 0;
        }

        #region 加入好友時，初始化ShopTemp
        internal void InsertInitialShopTemp()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertInitialShopTempCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string InsertInitialShopTempCmd()
        {
            return @"
                          INSERT INTO [dbo].[ShopTempTable]
                           ([UserID]
                           ,[ShopName]
                           ,[ShopPhone]
                           ,[ShopAddress]
                           ,[ClubID]
                           ,[ShopItem]
                           ,[ShopID])
                     VALUES
                           (@UserID
                           ,'NoData' 
                           ,'NoData'           
                           ,'NoData' 
                           ,'NoData' 
                           ,'NoData' 
                           ,'NoData') 
                    ";
        }
        #endregion


        #region 建立社團商店的時候要先把ClubID存進ShopTemp裡面的ClubID
        internal void UpdateClubIDByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateClubIDByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateClubIDByUserIDCmd()
        {
            return @"
                     UPDATE [dbo].[ShopTempTable]
                       SET
                          [ClubID] = @ClubID
                     WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 更改商店的時候要先把ShopID存進ShopTemp裡面的ShopID
        internal void UpdateShopIDByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateShopIDByUserIDCmd()))
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

        private string UpdateShopIDByUserIDCmd()
        {
            return @"
                     UPDATE [dbo].[ShopTempTable]
                       SET
                          [ShopID] = @ShopID
                     WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 更新商店名稱
        internal void UpdateShopName(string ShopName)
        {
            this.ShopName = ShopName;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateShopNameCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ShopName", this.ShopName);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateShopNameCmd()
        {
            return @"
                    UPDATE [dbo].[ShopTempTable]
                       SET
                          [ShopName] = @ShopName
                     WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 更新商店電話
        internal void UpdateShopPhone(string ShopPhone)
        {
            this.ShopPhone = ShopPhone;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateShopPhoneCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ShopPhone", this.ShopPhone);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateShopPhoneCmd()
        {
            return @"
                    UPDATE [dbo].[ShopTempTable]
                       SET
                          [ShopPhone] = @ShopPhone
                     WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 更新商店地址
        internal void UpdateShopAddress(string ShopAddress)
        {
            this.ShopAddress = ShopAddress;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateShopAddressCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ShopAddress", this.ShopAddress);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateShopAddressCmd()
        {
            return @"
                    UPDATE [dbo].[ShopTempTable]
                       SET
                          [ShopAddress] = @ShopAddress
                     WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 插入商店品項
        internal void InsertShopItem(string ShopItem)
        {
            this.ShopItem = ShopItem;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertShopItemTempCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ShopItem", this.ShopItem);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string InsertShopItemTempCmd()
        {
            return @"
                   INSERT INTO [dbo].[ShopItemTempTable]
                              ([UserID]
                              ,[ShopItem])
                        VALUES
                              (@UserID
                              ,@ShopItem)
                    ";
        }
        #endregion


        #region 插入商店品項的價錢
        internal void InsertShopItemPrice(string ShopItemPrice)
        {
            this.ShopItemPrice = int.Parse(ShopItemPrice);

            // 先把正在填寫的品項從UserStatus中的TempData選出來
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GetTempDataFromUserStatusTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.ShopItem = reader.GetString(0);
                        }
                    }
                }
            }

            // 再用UserID跟上面拿到的正在填寫的ShopItem，把ShopItmePrice建進資料庫
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateShopItemPriceTempCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ShopItem", this.ShopItem);
                    cmd.Parameters.AddWithValue("@ShopItemPrice", this.ShopItemPrice);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string GetTempDataFromUserStatusTableCmd()
        {
            return @"
                    SELECT [TempData]
                    FROM [dbo].[UserStatusTable]
                    WHERE UserID = @UserID; 
                    ";
        }

        private string UpdateShopItemPriceTempCmd()
        {
            return @"
                    UPDATE [dbo].[ShopItemTempTable]
                       SET
                          [ShopItemPrice] = @ShopItemPrice
                     WHERE UserID = @UserID and ShopItem = @ShopItem
                    ";
        }

        #endregion


        #region 取得ShopTemp裡的資料：ShopName, ShopPhone, ShopAddress
        internal void GetMyShopTempInfoFromSQL()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GetMyShopTempInfoFromSQLCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.ShopName = reader.GetString(1);
                            this.ShopPhone = reader.GetString(2);
                            this.ShopAddress = reader.GetString(3);
                        }
                    }
                }
            }
        }

        private string GetMyShopTempInfoFromSQLCmd()
        {
            return @"
                    SELECT [UserID]
                          ,[ShopName]
                          ,[ShopPhone]
                          ,[ShopAddress]
                      FROM [dbo].[ShopTempTable]
                      WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 取得ShopTemp裡的資料：ShopName, ShopPhone, ShopAddress, ClubID
        internal void GetClubShopTempInfoFromSQL()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GetClubShopTempInfoFromSQLCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.ShopName = reader.GetString(1);
                            this.ShopPhone = reader.GetString(2);
                            this.ShopAddress = reader.GetString(3);
                            this.ClubID = reader.GetString(4);
                        }
                    }
                }
            }
        }

        private string GetClubShopTempInfoFromSQLCmd()
        {
            return @"
                    SELECT [UserID]
                          ,[ShopName]
                          ,[ShopPhone]
                          ,[ShopAddress]
                          ,[ClubID]
                      FROM [dbo].[ShopTempTable]
                      WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 取得ShopItemTemp裡的資料：ShopItem, ShopItemPrice
        internal List<ShopTemp> GetShopItemTempFromSQL()
        {
            List<ShopTemp> ShopItemTempList = new List<ShopTemp>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GetShopItemTempFromSQLCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopTemp shopTemp = new ShopTemp();
                            shopTemp.ShopItem = reader.GetString(1);
                            shopTemp.ShopItemPrice = reader.GetDouble(2);
                            ShopItemTempList.Add(shopTemp);
                        }
                    }
                }
            }
            return ShopItemTempList;
        }

        private string GetShopItemTempFromSQLCmd()
        {
            return @"
                    SELECT [UserID]
                          ,[ShopItem]
                          ,[ShopItemPrice]
                      FROM [dbo].[ShopItemTempTable]
                      WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region MyShop：把所有Temp資料分別存進MyShopTable, MyShopDetail, MyShopItemTable
        internal int InsertAllMyShopData(List<ShopTemp> shopItemList)
        {
            int result1 = 0;
            int result2 = 0;
            int result3 = 0;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertMyShopTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@MyShopName", this.ShopName);
                    DBHelper dBHelper = new DBHelper();
                    result1 = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertMyShopDetailTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@MyShopName", this.ShopName);
                    cmd.Parameters.AddWithValue("@MyShopPhone", this.ShopPhone);
                    cmd.Parameters.AddWithValue("@MyShopAddress", this.ShopName);
                    DBHelper dBHelper = new DBHelper();
                    result2 = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            foreach (var item in shopItemList)
            {
                using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(InsertMyShopItemTableCmd()))
                    {
                        connection.Open();
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);
                        cmd.Parameters.AddWithValue("@MyShopItem", item.ShopItem);
                        cmd.Parameters.AddWithValue("@MyShopItemPrice", item.ShopItemPrice);
                        DBHelper dBHelper = new DBHelper();
                        result3 += dBHelper.ExecuteNonQuery(cmd);
                    }
                }
            }
            int result = result1 * result2 * result3;
            return result;
        }

        private string InsertMyShopTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[MyShopTable]
                               ([UserID]
                               ,[MyShopID]
                               ,[MyShopName])
                         VALUES
                               (@UserID
                               ,@MyShopID
                               ,@MyShopName)
                    ";
        }

        private string InsertMyShopDetailTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[MyShopDetailTable]
                               ([MyShopID]
                               ,[MyShopName]
                               ,[MyShopPhone]
                               ,[MyShopAddress])
                         VALUES
                               (@MyShopID
                               ,@MyShopName
                               ,@MyShopPhone
                               ,@MyShopAddress)
                    ";
        }

        private string InsertMyShopItemTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[MyShopItemTable]
                               ([MyShopID]
                               ,[MyShopItem]
                               ,[MyShopItemPrice])
                         VALUES
                               (@MyShopID
                               ,@MyShopItem
                               ,@MyShopItemPrice)
                    ";
        }
        #endregion


        #region ClubShop：把所有Temp資料分別存進ClubShopTable, ClubShopDetail, ClubShopItemTable
        internal int InsertAllClubShopData(List<ShopTemp> shopItemList)
        {
            int result1 = 0;
            int result2 = 0;
            int result3 = 0;

            

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertClubShopTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubID);
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@ClubShopName", this.ShopName);
                    DBHelper dBHelper = new DBHelper();
                    result1 = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertClubShopDetailTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@ClubShopName", this.ShopName);
                    cmd.Parameters.AddWithValue("@ClubShopPhone", this.ShopPhone);
                    cmd.Parameters.AddWithValue("@ClubShopAddress", this.ShopAddress);
                    DBHelper dBHelper = new DBHelper();
                    result2 = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            foreach (var item in shopItemList)
            {
                using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(InsertClubShopItemTableCmd()))
                    {
                        connection.Open();
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);
                        cmd.Parameters.AddWithValue("@ClubShopItem", item.ShopItem);
                        cmd.Parameters.AddWithValue("@ClubShopItemPrice", item.ShopItemPrice);
                        DBHelper dBHelper = new DBHelper();
                        result3 += dBHelper.ExecuteNonQuery(cmd);
                    }
                }
            }
            int result = result1 * result2 * result3;
            return result;
        }

        private string InsertClubShopTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[ClubShopTable]
                               ([ClubID]
                               ,[ClubShopID]
                               ,[ClubShopName])
                         VALUES
                               (@ClubID
                               ,@ClubShopID
                               ,@ClubShopName)
                    ";
        }

        private string InsertClubShopDetailTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[ClubShopDetailTable]
                               ([ClubShopID]
                               ,[ClubShopName]
                               ,[ClubShopPhone]
                               ,[ClubShopAddress])
                         VALUES
                               (@ClubShopID
                               ,@ClubShopName
                               ,@ClubShopPhone
                               ,@ClubShopAddress)
                    ";
        }

        private string InsertClubShopItemTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[ClubShopItemTable]
                               ([ClubShopID]
                               ,[ClubShopItem]
                               ,[ClubShopItemPrice])
                         VALUES
                               (@ClubShopID
                               ,@ClubShopItem
                               ,@ClubShopItemPrice)
                    ";
        }
        #endregion


        #region 建立商店結束，把ShopItemTemp裡的資料清空
        internal void DeleteShopItemTempByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteShopItemTempByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string DeleteShopItemTempByUserIDCmd()
        {
            return @"
                    DELETE FROM [dbo].[ShopItemTempTable]
                            WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 以UserID將ShopTemp裡的資料初始化為NoData
        internal void InitializeShopTempByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InitializeShopTempByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string InitializeShopTempByUserIDCmd()
        {
            return @"
                    UPDATE [dbo].[ShopTempTable]
                       SET 
                          [ShopName] = 'NoData'
                          ,[ShopPhone] = 'NoData'
                          ,[ShopAddress] = 'NoData'
                          ,[ClubID] = 'NoData'
                          ,[ShopID] = 'NoData'
                          ,[ShopItem] = 'NoData'
                    WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 用UserID拿出ShopTempTable所有東西
        internal void SelectByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByUserIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShopName = reader.GetString(1);
                            ShopPhone = reader.GetString(2);
                            ShopAddress = reader.GetString(3);
                            ClubID = reader.GetString(4);
                            ShopID = reader.GetString(5);
                            ShopItem = reader.GetString(6);
                        }
                    }
                }
            }
        }

        private string SelectByUserIDcmd()
        {
            return @"
                    SELECT [UserID]
                          ,[ShopName]
                          ,[ShopPhone]
                          ,[ShopAddress]
                          ,[ClubID]
                          ,[ShopID]
                          ,[ShopItem]
                      FROM [dbo].[ShopTempTable]
                      WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 更新商店品項前先存下更新前的品項
        internal void UpdateItemToShopTemp()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateItemToShopTempCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ShopItem", this.ShopItem);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateItemToShopTempCmd()
        {
            return @"
                    UPDATE [dbo].[ShopTempTable]
                       SET
                          [ShopItem] = @ShopItem
                     WHERE UserID = @UserID
                    ";
        }
        #endregion


        #region 更新MyShop商店品項
        internal void UpdateMyShopItem(ShopTemp ShopTempNew)
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateMyShopItemCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@MyShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@MyShopItemNew", ShopTempNew.ShopItem);
                    cmd.Parameters.AddWithValue("@MyShopItem", this.ShopItem);
                    cmd.Parameters.AddWithValue("@MyShopItemPrice", ShopTempNew.ShopItemPrice);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }
  
        private string UpdateMyShopItemCmd()
        {
            return @"
                    UPDATE [dbo].[MyShopItemTable]
                       SET
                          [MyShopItem] = @MyShopItemNew
                          ,[MyShopItemPrice] = @MyShopItemPrice
                     WHERE MyShopID = @MyShopID and MyShopItem = @MyShopItem
                    ";
        }
        #endregion


        #region 更新ClubShop商店品項
        internal void UpdateClubShopItem(ShopTemp ShopTempNew)
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateClubShopItemCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@ClubShopItemNew", ShopTempNew.ShopItem);
                    cmd.Parameters.AddWithValue("@ClubShopItem", this.ShopItem);
                    cmd.Parameters.AddWithValue("@ClubShopItemPrice", ShopTempNew.ShopItemPrice);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateClubShopItemCmd()
        {
            return @"
                    UPDATE [dbo].[ClubShopItemTable]
                       SET
                          [ClubShopItem] = @ClubShopItemNew
                          ,[ClubShopItemPrice] = @ClubShopItemPrice
                     WHERE ClubShopID = @ClubShopID and ClubShopItem = @ClubShopItem
                    ";
        }
        #endregion



    }
}