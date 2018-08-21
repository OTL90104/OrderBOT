using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class OrderInfo
    {
        private string UserIDOrClubID;
        public string OrderID;
        public string ShopID;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string OrderStatus { get; set; }
        public string OrderType { get; set; }
        public string OrderPartitionID { get; set; }
        public string OrderName { get; set; }
        public string ShopName { get; set; }

        public OrderInfo(string UserIDOrClubID)
        {
            this.UserIDOrClubID = UserIDOrClubID;
        }

        public OrderInfo(string UserIDOrClubID, string OrderName)
        {
            this.UserIDOrClubID = UserIDOrClubID;
            this.OrderName = OrderName;
           // this.OrderID = $"O{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{UserIDOrClubID}";
            this.OrderID = $"O{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{UserIDOrClubID}";
        }

        public OrderInfo()
        {

        }

        #region InserMyOrderTableToSQL
        internal int InserMyOrderTableToSQL()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertMyOrderTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserIDOrClubID);
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    cmd.Parameters.AddWithValue("@OrderStatus", this.OrderStatus);
                    cmd.Parameters.AddWithValue("@OrderType", this.OrderType);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@OrderName", this.OrderName);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string InsertMyOrderTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[MyOrderTable]
                               ([UserID]
                               ,[OrderID]
                               ,[OrderType]
                               ,[StartDateTime]
                               ,[EndDateTime]
                               ,[ShopID]
                               ,[OrderStatus]
                               ,[CreateTime]
                               ,[OrderName]
                               ,[OrderPartitionID])
                               
                         VALUES
                               (@UserID
                               ,@OrderID
                               ,@OrderType
                               ,@StartTime
                               ,@EndTime
                               ,@ShopID
                               ,@OrderStatus
                               ,SYSDATETIME()
                               ,@OrderName
                               ,@OrderPartitionID)
                    ";
        }
        #endregion


        #region InserClubOrderTableToSQL
        internal int InserClubOrderTableToSQL()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertClubOrderTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubID", this.UserIDOrClubID);
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    cmd.Parameters.AddWithValue("@OrderStatus", this.OrderStatus);
                    cmd.Parameters.AddWithValue("@OrderType", this.OrderType);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@OrderName", this.OrderName);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string InsertClubOrderTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[ClubOrderTable]
                                ([ClubID]
                               ,[OrderID]
                               ,[OrderType]
                               ,[StartDateTime]
                               ,[EndDateTime]
                               ,[ShopID]
                               ,[OrderStatus]
                               ,[CreateTime]
                               ,[OrderPartitionID]
                               ,[OrderName])
                            VALUES
                               (@ClubID
                               ,@OrderID
                               ,@OrderType
                               ,@StartTime
                               ,@EndTime
                               ,@ShopID
                               ,@OrderStatus
                               ,SYSDATETIME()
                               ,@OrderPartitionID
                               ,@OrderName)
                     ";
        }
        #endregion


        #region SelectMyOrderByUserID
        public List<OrderInfo> SelectMyOrderByUserID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectMyOrderByUserIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserIDOrClubID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo(this.UserIDOrClubID);
                            orderinfo.OrderID = reader.GetString(1);
                            orderinfo.OrderType = reader.GetString(2);
                            orderinfo.OrderName = reader.GetString(3);

                            list.Add(orderinfo);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectMyOrderByUserIDcmd()
        {
            return @"
                    SELECT 
                       M.UserID
                      ,M.OrderID
                      ,M.OrderType
                      ,M.OrderName
                      FROM [Linebot].[dbo].[MyOrderTable] AS M
                      WHERE M.UserID = @UserID
                      GROUP BY M.UserID,M.OrderID,M.OrderType,M.OrderName
                    ";
        }
        #endregion


        #region SelectClubOrderByUserID
        public List<OrderInfo> SelectClubOrderByUserID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectClubOrderByClubIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubID", this.UserIDOrClubID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo();
                            orderinfo.OrderID = reader.GetString(1);
                            orderinfo.OrderType = reader.GetString(2);
                            orderinfo.OrderName = reader.GetString(3);
                            list.Add(orderinfo);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectClubOrderByClubIDcmd()
        {
            return @"
                    SELECT 
                       M.ClubID
                      ,M.OrderID
                      ,M.OrderType
                      ,M.OrderName

                      FROM [Linebot].[dbo].[ClubOrderTable] AS M
                        WHERE ClubID = @ClubID
                      GROUP BY M.ClubID,M.OrderID,M.OrderType,M.OrderName;
                    ";
        }
        #endregion


        #region 得到MyOrder 的Partition Order 
        internal List<OrderInfo> SelectMyOrderByOrderID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectMyOrderByOrderIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo();
                            orderinfo.OrderID = reader.GetString(1);
                            orderinfo.OrderType = reader.GetString(2);
                            orderinfo.StartTime = reader.GetDateTime(3);
                            orderinfo.EndTime = reader.GetDateTime(4);
                            orderinfo.ShopID = reader.GetString(5);
                            orderinfo.OrderStatus = reader.GetString(6);
                            orderinfo.OrderPartitionID = reader.GetString(7);
                            orderinfo.OrderName = reader.GetString(8);
                            list.Add(orderinfo);
                        }
                    }
                }
                return list;
            }
        }

        private string SelectMyOrderByOrderIDcmd()
        {
            return @"
                    SELECT [UserID]
                          ,[OrderID]
                          ,[OrderType]
                          ,[StartDateTime]
                          ,[EndDateTime]
                          ,[ShopID]
                          ,[OrderStatus]
                          ,[OrderPartitionID]
                          ,[OrderName]
                          
                      FROM [Linebot].[dbo].[MyOrderTable]
                    WHERE OrderID = @OrderID;
                    ";
        }
        #endregion


        #region 得到ClubOrder 的Partition Order 
        internal List<OrderInfo> SelectClubOrderByOrderID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectClubOrderByOrderIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo();
                            orderinfo.OrderID = reader.GetString(1);
                            orderinfo.OrderType = reader.GetString(2);
                            orderinfo.StartTime = reader.GetDateTime(3);
                            orderinfo.EndTime = reader.GetDateTime(4);
                            orderinfo.ShopID = reader.GetString(5);
                            orderinfo.OrderStatus = reader.GetString(6);
                            orderinfo.OrderPartitionID = reader.GetString(7);
                            orderinfo.OrderName = reader.GetString(8);
                            list.Add(orderinfo);
                        }
                    }
                }
                return list;
            }
        }

        private string SelectClubOrderByOrderIDcmd()
        {
            return @"
                    SELECT [ClubID]
                          ,[OrderID]
                          ,[OrderType]
                          ,[StartDateTime]
                          ,[EndDateTime]
                          ,[ShopID]
                          ,[OrderStatus]
                          ,[OrderPartitionID]
                          ,[OrderName]
                          
                      FROM [Linebot].[dbo].[ClubOrderTable]
                    WHERE OrderID = @OrderID;
                    ";
        }
        #endregion


        #region 使用者要使用訂單參加碼參加訂單的時候，要先檢查使用者輸入的OrderID是不是有在OrderUser裡面
        internal int CheckOrderUserTable(string userInputOrderID)
        {
            List<OrderInfo> orderInfoList = new List<OrderInfo>();
            this.OrderID = userInputOrderID;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(CheckOrderUserTablecmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo(this.UserIDOrClubID);
                            orderinfo.UserIDOrClubID = reader.GetString(0);
                            orderInfoList.Add(orderinfo);
                        }
                    }
                }
            }
            return orderInfoList.Count;
        }

        private string CheckOrderUserTablecmd()
        {
            return @"
                    SELECT 
                       UserID
                      FROM [Linebot].[dbo].[OrderUserTable]
                      WHERE OrderID = @OrderID
                     ";
        }
        #endregion


        #region 用OrderID到MyOrderTable取得OrderName
        internal void SelectMyOrderNameByOrderID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectMyOrderNameByOrderIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.OrderName = reader.GetString(0);
                        }
                    }
                }
            }
        }

        private string SelectMyOrderNameByOrderIDcmd()
        {
            return @"
                    SELECT 
                          [OrderName]                          
                      FROM [Linebot].[dbo].[MyOrderTable]
                    WHERE OrderID = @OrderID;
                    ";
        }
        #endregion


        #region 用OrderPartitionID到MyOrderTable取得資料
        internal void SelectMyOrderTableByOrderPartitionID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectMyOrderTableByOrderPartitionIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.OrderID = reader.GetString(0);
                            this.OrderType = reader.GetString(1);
                            this.StartTime = reader.GetDateTime(2);
                            this.EndTime = reader.GetDateTime(3);
                            this.ShopID = reader.GetString(4);
                            this.OrderStatus = reader.GetString(5);
                            this.OrderName = reader.GetString(6);
                        }
                    }
                }
            }
        }

        private string SelectMyOrderTableByOrderPartitionIDcmd()
        {
            return @"
                    SELECT 
                          [OrderID]
                         ,[OrderType]
                         ,[StartDateTime]
                         ,[EndDateTime]
                         ,[ShopID]
                         ,[OrderStatus]
                         ,[OrderName]                         
                      FROM [Linebot].[dbo].[MyOrderTable]
                    WHERE OrderPartitionID = @OrderPartitionID;
                    ";
        }
        #endregion


        #region 用OrderPartitionID到ClubOrderTable取得資料
        internal void SelectClubOrderTableByOrderPartitionID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectClubOrderTableByOrderPartitionIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.OrderID = reader.GetString(0);
                            this.OrderType = reader.GetString(1);
                            this.StartTime = reader.GetDateTime(2);
                            this.EndTime = reader.GetDateTime(3);
                            this.ShopID = reader.GetString(4);
                            this.OrderStatus = reader.GetString(5);
                            this.OrderName = reader.GetString(6);
                        }
                    }
                }
            }
        }

        private string SelectClubOrderTableByOrderPartitionIDcmd()
        {
            return @"
                    SELECT 
                          [OrderID]
                         ,[OrderType]
                         ,[StartDateTime]
                         ,[EndDateTime]
                         ,[ShopID]
                         ,[OrderStatus]
                         ,[OrderName]                         
                      FROM [Linebot].[dbo].[ClubOrderTable]
                    WHERE OrderPartitionID = @OrderPartitionID;
                    ";
        }
        #endregion


        #region insert參加訂單的人到訂單底下
        internal int InsertOrdeUserTable()
        {
            int result;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertOrdeUserTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserIDOrClubID);
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string InsertOrdeUserTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[OrderUserTable]
                               ([OrderID]
                               ,[UserID])
                         VALUES
                               (@OrderID
                               ,@UserID)
                    ";
        }

        #endregion


        #region UpdateShopIDByOrderPartitionIDToMyOrder
        internal int UpdateShopIDByOrderPartitionIDToMyOrder()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateShopIDByOrderPartitionIDToMyOrderCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateShopIDByOrderPartitionIDToMyOrderCmd()
        {
            return @"
                    UPDATE [dbo].[MyOrderTable]
                       SET 
                           [ShopID] = @ShopID                                    
                     WHERE [OrderPartitionID] = @OrderPartitionID;
                    ";
        }
        #endregion


        #region UpdateShopIDByOrderPartitionIDToClubOrder
        internal int UpdateShopIDByOrderPartitionIDToClubOrder()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateShopIDByOrderPartitionIDToClubOrderCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@ShopID", this.ShopID);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateShopIDByOrderPartitionIDToClubOrderCmd()
        {
            return @"
                    UPDATE [dbo].[ClubOrderTable]
                       SET 
                           [ShopID] = @ShopID               
                    WHERE [OrderPartitionID] = @OrderPartitionID;
                    ";
        }
        #endregion


        #region UpdateTimeByOrderPartitionIDToMyOrder
        internal int UpdateTimeByOrderPartitionIDToMyOrder()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateTimeByOrderPartitionIDToMyOrderCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@OrderStatus", this.OrderStatus);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateTimeByOrderPartitionIDToMyOrderCmd()
        {
            return @"
                    UPDATE [dbo].[MyOrderTable]
                       SET 
                           [StartDateTime] = @StartTime
                          ,[EndDateTime] = @EndTime        
                          ,[OrderStatus] = @OrderStatus
                     WHERE [OrderPartitionID] = @OrderPartitionID;
                    ";
        }
        #endregion


        #region 取得使用者所有參加的訂單用(Order)
        public List<OrderInfo> SelectUserMyOrderByUserID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectUserMyOrderByUserIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserIDOrClubID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo(this.UserIDOrClubID);
                            orderinfo.OrderID = reader.GetString(1);
                            orderinfo.OrderName = reader.GetString(2);
                            orderinfo.OrderStatus = reader.GetString(3);

                            list.Add(orderinfo);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectUserMyOrderByUserIDcmd()
        {
            return @"
                   SELECT 
                     UserID
                    ,OrderID
                    ,OrderName
                    ,OrderStatus
                    FROM (SELECT OU.UserID, OU.OrderID, MO.OrderName, MO.OrderStatus
                    FROM OrderUserTable AS OU
                    LEFT JOIN MyOrderTable AS MO ON OU.OrderID = MO.OrderID
                    GROUP BY OU.UserID, OU.OrderID, MO.OrderName, MO.OrderStatus) AS a
                    WHERE a.UserID = @UserID and OrderName is not NULL and (OrderStatus = 'wait' or OrderStatus = 'available');
                    ";
        }

        public List<OrderInfo> SelectUserClubOrderByUserID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectUserClubOrderByUserIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserIDOrClubID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo(this.UserIDOrClubID);
                            orderinfo.OrderID = reader.GetString(1);
                            orderinfo.OrderName = reader.GetString(2);
                            orderinfo.OrderStatus = reader.GetString(3);

                            list.Add(orderinfo);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectUserClubOrderByUserIDcmd()
        {
            return @"
                 SELECT ClubID
                        ,OrderID
                        ,OrderName
                        ,OrderStatus
	                    ,UserID
                        FROM (	  
	                    SELECT CU.ClubID, CO.OrderID, CO.OrderName, CO.OrderStatus, CU.UserID
                        FROM ClubUserTable AS CU
                        LEFT JOIN ClubOrderTable AS CO ON CU.ClubID = CO.ClubID
                        GROUP BY CU.ClubID, CO.OrderID, CO.OrderName, CO.OrderStatus, CU.UserID) AS a
                        WHERE a.UserID = @UserID and (OrderStatus = 'wait' or OrderStatus = 'available');
                    ";
        }
        #endregion


        #region 取得使用者所有參加的訂單細項用(PartitionOrder)
        public List<OrderInfo> SelectUserMyPartitionOrderByOrderID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectUserMyPartitionOrderByOrderIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo(this.UserIDOrClubID);
                            orderinfo.OrderPartitionID = reader.GetString(0);
                            orderinfo.StartTime = reader.GetDateTime(1);
                            orderinfo.ShopID = reader.GetString(2);
                            orderinfo.ShopName = reader.GetString(3);
                            list.Add(orderinfo);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectUserMyPartitionOrderByOrderIDcmd()
        {
            return @"
                   SELECT MO.OrderPartitionID, MO.StartDateTime, MO.ShopID, MS.MyShopName AS ShopName
                    FROM MyOrderTable AS MO
                    LEFT JOIN MyShopTable AS MS ON MO.ShopID = MS.MyShopID
                    WHERE MS.MyShopName is not NULL and MO.OrderID = @OrderID
                    
                    union all 
                    SELECT MO.OrderPartitionID, MO.StartDateTime, MO.ShopID, CS.ClubShopName AS ShopName
                    FROM MyOrderTable AS MO
                    LEFT JOIN ClubShopTable AS CS ON MO.ShopID = CS.ClubShopID
                    WHERE CS.ClubShopName is not NULL and MO.OrderID = @OrderID
                    
                    union all
                    SELECT MO.OrderPartitionID, MO.StartDateTime, MO.ShopID, BS.BossShopName AS ShopName
                    FROM MyOrderTable AS MO
                    LEFT JOIN BossShopTable AS BS ON MO.ShopID = BS.BossShopID
                    WHERE BS.BossShopName is not NULL and MO.OrderID = @OrderID
                    ";
        }

        public List<OrderInfo> SelectUserClubPartitionOrderByOrderID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectUserClubPartitionOrderByOrderIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo(this.UserIDOrClubID);
                            orderinfo.OrderPartitionID = reader.GetString(0);
                            orderinfo.StartTime = reader.GetDateTime(1);
                            orderinfo.ShopID = reader.GetString(2);
                            orderinfo.ShopName = reader.GetString(3);
                            list.Add(orderinfo);
                        }
                    }
                }
            }
            return list;
        }

        private string SelectUserClubPartitionOrderByOrderIDcmd()
        {
            return @"
                  SELECT CO.OrderPartitionID, CO.StartDateTime, CO.ShopID, MS.MyShopName AS ShopName
                    FROM ClubOrderTable AS CO
                    LEFT JOIN MyShopTable AS MS ON CO.ShopID = MS.MyShopID
                    WHERE MS.MyShopName is not NULL and CO.OrderID = @OrderID
                    
                    union all 
                    SELECT CO.OrderPartitionID, CO.StartDateTime, CO.ShopID, CS.ClubShopName AS ShopName
                    FROM ClubOrderTable AS CO
                    LEFT JOIN ClubShopTable AS CS ON CO.ShopID = CS.ClubShopID
                    WHERE CS.ClubShopName is not NULL and CO.OrderID = @OrderID
                    
                    union all
                    SELECT CO.OrderPartitionID, CO.StartDateTime, CO.ShopID, BS.BossShopName AS ShopName
                    FROM ClubOrderTable AS CO
                    LEFT JOIN BossShopTable AS BS ON CO.ShopID = BS.BossShopID
                    WHERE BS.BossShopName is not NULL and CO.OrderID = @OrderID
                    ";
        }
        #endregion


        #region 刪除其中一筆我的週期性訂單
        internal int DeleteMyOrderPartitionByUserIDandOrderPartitionID()
        {
            int result;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteMyOrderPartitionByUserIDandOrderPartitionIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserIDOrClubID);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);

                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }



        private string DeleteMyOrderPartitionByUserIDandOrderPartitionIDCmd()
        {
            return @"
                    DELETE FROM [dbo].[MyOrderTable]
                          WHERE UserID = @UserID and OrderPartitionID = @OrderPartitionID
                    ";
        }
        #endregion


        #region 刪除完整我的訂單
        internal int DeleteMyOrderByUserIDandOrderID()
        {
            int result;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteMyPeriodOrderByUserIDandOrderIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserIDOrClubID);
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string DeleteMyPeriodOrderByUserIDandOrderIDCmd()
        {
            return @"
                    DELETE FROM [dbo].[OrderUserTable]
                          WHERE UserID = @UserID and OrderID = @OrderID;

                    DELETE FROM [dbo].[MyOrderTable]
                          WHERE UserID = @UserID and OrderID = @OrderID
                    ";
        }
        #endregion


        #region UpdateTimeByOrderPartitionIDTOClubOrder
        internal int UpdateTimeByOrderPartitionIDTOClubOrder()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateTimeByOrderPartitionIDTOClubOrderCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    cmd.Parameters.AddWithValue("@OrderStatus", this.OrderStatus);
                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string UpdateTimeByOrderPartitionIDTOClubOrderCmd()
        {
            return @"
                    UPDATE [dbo].[ClubOrderTable]
                       SET 
                           [StartDateTime] = @StartTime
                          ,[EndDateTime] = @EndTime
                          ,[OrderStatus] = @OrderStatus
                     WHERE [OrderPartitionID] = @OrderPartitionID;
                    ";
        }
        #endregion



        #region 訂單發起人刪除整筆我的訂單

        internal int DeleteMyOrderByOrderID()
        {
            int result;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteMyOrderByOrderIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string DeleteMyOrderByOrderIDCmd()
        {
            return @"
                    DELETE FROM [dbo].[OrderUserTable]
                          WHERE  OrderID = @OrderID;

                    DELETE FROM [dbo].[MyOrderTable]
                          WHERE  OrderID = @OrderID
                    ";
        }
        #endregion


        #region 刪除整筆社團訂單

        internal int DeleteClubOrderByOrderID()
        {
            int result;

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteClubOrderByOrderIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    DBHelper dBHelper = new DBHelper();
                    result = dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return result;
        }

        private string DeleteClubOrderByOrderIDCmd()
        {
            return @"


                    DELETE FROM [dbo].[ClubOrderTable]
                          WHERE  OrderID = @OrderID
                    ";
        }
        #endregion

        #region 用OrderID到ClubOrderTable取得OrderName
        internal void SelectClubOrderNameByOrderID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectClubOrderNameByOrderIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.OrderName = reader.GetString(0);
                        }
                    }
                }
            }
        }

        private string SelectClubOrderNameByOrderIDcmd()
        {
            return @"
                    SELECT 
                          [OrderName]                          
                      FROM [Linebot].[dbo].[ClubOrderTable]
                    WHERE OrderID = @OrderID;
                    ";
        }
        #endregion

        public List<OrderInfo> CheckClubOrderByUserID()
        {
            List<OrderInfo> list = new List<OrderInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(CheckClubOrderByUserIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubID", this.UserIDOrClubID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderInfo orderinfo = new OrderInfo();
                            orderinfo.OrderID = reader.GetString(1);
                            orderinfo.OrderType = reader.GetString(2);
                            orderinfo.OrderName = reader.GetString(3);
                            orderinfo.OrderStatus = reader.GetString(4);
                            orderinfo.OrderPartitionID = reader.GetString(5);
                            list.Add(orderinfo);
                        }
                    }
                }
            }
            return list;
        }

        private string CheckClubOrderByUserIDcmd()
        {
            return @"
                    SELECT 
                       ClubID
                      ,OrderID
                      ,OrderType
                      ,OrderName
                      ,OrderStatus
                      ,OrderPartitionID
                      FROM [Linebot].[dbo].[ClubOrderTable] 
                        WHERE ClubID = @ClubID and OrderStatus = 'available'
    
                    ";
        }
    }
}
