using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class OrderTemp
    {
        private string UserID;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ClubID { get; set; }
        public string ShopID { get; set; }
        public string OrderPartitionID { get; set; }



        public OrderTemp(string userId)
        {
            this.UserID = userId;
            this.StartTime = Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value);
            this.EndTime = Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value);
        }

        public OrderTemp()
        {
        }
        internal void ClubUpdateInitialOrderTmp()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(ClubUpdateInitialOrderTmpCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

        }

        private string ClubUpdateInitialOrderTmpCmd()
        {
            return @"
UPDATE [dbo].[OrderTempTable]
   SET 
       [StartTime] = @StartTime
      ,[EndTime] = @EndTime
 WHERE UserID = @UserID
";
        }
        internal void UpdateInitialOrderTemp()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateInitialOrderTmpCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

        }


        private string UpdateInitialOrderTmpCmd()
        {
            return @"
UPDATE [dbo].[OrderTempTable]
   SET 
       [StartTime] = @StartTime
      ,[EndTime] = @EndTime
      ,[ClubID]= 'NoData'
      ,[OrderPartitionID] = 'NoData'
      ,[ShopID] = 'NoData'
 WHERE UserID = @UserID
";
        }

        internal void UpdateOrderPartitionIDByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateOrderPartitionIDByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@OrderPartitionID", this.OrderPartitionID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateOrderPartitionIDByUserIDCmd()
        {
            return @"
UPDATE [dbo].[OrderTempTable]
   SET 
       [OrderPartitionID] = @OrderPartitionID
 WHERE UserID = @UserID
";
        }

        internal void InsertInitialOrderTmp()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertInitialOrderTmpCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

        }

        internal void UpdateClubIDByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateClubIDByUderIDcmd()))
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

        private string UpdateClubIDByUderIDcmd()
        {
            return @"
                    UPDATE [dbo].[OrderTempTable]
                       SET [ClubID] = @ClubID
                          
                     WHERE [UserID] = @UserID;
                    ";
        }

        private string InsertInitialOrderTmpCmd()
        {
            return @"
                    INSERT INTO [dbo].[OrderTempTable]
                               ([UserID]
                               ,[StartTime]
                               ,[EndTime]
                               ,[OrderPartitionID]
                               ,[ClubID]
                               ,[ShopID]
                               )
                         VALUES
                               (@UserID
                               ,@StartTime
                               ,@EndTime
                               ,'NoData'
                               ,'NoData'
                               ,'NoData'
                               )
                    ";
        }

        internal void UpdateEndTime()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateEndTimeCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

        }

        private string UpdateEndTimeCmd()
        {
            return @"
                     UPDATE [dbo].[OrderTempTable]
                     SET 
                        [EndTime] = @EndTime
                     WHERE [UserID] = @UserID
                    ";
        }

        internal void UpdateStartTime()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateStartTimeCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

        }

        internal bool CheckBothTimeHasFilledIn(OrderTemp orderTmp)
        {
            orderTmp.SelectByUserID();
            DateTime SQLInitialTime = Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value);
            if (orderTmp.StartTime == SQLInitialTime || orderTmp.EndTime == SQLInitialTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ClubIDSelectByUserID()
        {

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(ClubIDSelectByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.ClubID = reader.GetString(0);
                        }
                    }
                }

            }
        }

        public void SelectByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.StartTime = reader.GetDateTime(1);
                            this.EndTime = reader.GetDateTime(2);
                            this.ShopID = reader.GetString(3);
                            this.OrderPartitionID = reader.GetString(4);
                        }
                    }
                }
            }
        }
        private string ClubIDSelectByUserIDCmd()
        {
            return @"
SELECT [ClubID]
  FROM [Linebot].[dbo].[OrderTempTable]
  WHERE UserID = @UserID;
";

        }
        private string SelectByUserIDCmd()
        {
            return @"
SELECT [UserID]
      ,[StartTime]
      ,[EndTime]
      ,[ShopID]
      ,[OrderPartitionID]
  FROM [Linebot].[dbo].[OrderTempTable]
  WHERE UserID = @UserID;
";
        }

        private string UpdateStartTimeCmd()
        {
            return @"
   UPDATE [dbo].[OrderTempTable]
   SET 
      [StartTime] = @StartTime
   WHERE [UserID] = @UserID
";
        }

        internal void UpdateShopID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateShopIDCmd()))
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

        private string UpdateShopIDCmd()
        {
            return @"
   UPDATE [dbo].[OrderTempTable]
   SET 
      [ShopID] = @ShopID
   WHERE [UserID] = @UserID
";
        }

//        internal void SelectByUserID()
//        {
//            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(SelectPartitionIDByUserIDCmd()))
//                {
//                    connection.Open();
//                    cmd.Connection = connection;
//                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            // OrderTmp orderTmp = new OrderTmp();
//                            this.StartTime = reader.GetDateTime(1);
//                            this.EndTime = reader.GetDateTime(2);
//                            this.OrderPartitionID = reader.GetString(3);
//                            //this.EndTime = Convert.ToDateTime(reader.GetString(2));
//                        }
//                    }
//                }
//            }
//        }

//        private string SelectPartitionIDByUserIDCmd()
//        {
//            return @"
//SELECT
//       [UserID]
//      ,[StartTime]
//      ,[EndTime]
//      ,[OrderPartitionID]
//        FROM[Linebot].[dbo].[OrderTempTable]
//        WHERE UserID = @UserID;
//";
//        }
    }
}