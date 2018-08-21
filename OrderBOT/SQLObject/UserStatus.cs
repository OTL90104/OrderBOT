using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class UserStatus
    {
        public string UserID { get; set; }
        public string UserDisplayName { get; set; }
        public int QID { get; set; }
        public int OID { get; set; }
        public string TempData { get; set; }

        public UserStatus()
        {

        }

        public UserStatus(string UserID)
        {
            this.UserID = UserID;
        }

        public UserStatus(string UserID, string UserDisplayName)
        {
            this.UserID = UserID;
            this.UserDisplayName = UserDisplayName;
        }

        public UserStatus(string UserID, int QID, int OID)
        {
            this.UserID = UserID;
            this.QID = QID;
            this.OID = OID;
        }

        public UserStatus(string UserID, int QID, int OID, string TempData)
        {
            this.UserID = UserID;
            this.QID = QID;
            this.OID = OID;
            this.TempData = TempData;
        }

        #region 機器人加入好友的時候，先把使用者資訊加入UserStatus，以確保資料庫有資料，之後可以全部使用update
        public List<UserStatus> InitializeByUserID()
        {
            List<UserStatus> list = new List<UserStatus>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertUserStatusCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateUserStatusCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertUserInfoCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@UserDisplayName", this.UserDisplayName);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

            return list;
        }

        private string InsertUserStatusCmd()
        {
            return @"INSERT INTO [dbo].[UserStatusTable]
                               ([UserID]
                               ,[QID]
                               ,[OID]
                               ,[TempData])
                         VALUES
                               (@UserID
                               ,0
                               ,0
                               ,'0')";
        }
        private string UpdateUserStatusCmd()
        {
            return @"
                    UPDATE UserStatusTable
                            SET [QID] = 0
                                ,[OID] = 0
                                ,[TempData] = '0'
                    WHERE UserID = @UserID;
                    ";
        }
        private string InsertUserInfoCmd()
        {
            return @"INSERT INTO [dbo].[UserInfoTable]
                          ([UserID]
                          ,[UserDisplayName])
                    VALUES
                          (@UserID
                          ,@UserDisplayName)";
        }
        #endregion


        #region 在使用messageMaker時，因為沒有按鈕可以攜帶資訊，所以需記錄使用者當下狀態，以便之後取得使用者狀態
        public List<UserStatus> UpdateByUserID()
        {
            List<UserStatus> list = new List<UserStatus>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@QID", this.QID);
                    cmd.Parameters.AddWithValue("@OID", this.OID);
                    cmd.Parameters.AddWithValue("@TempData", this.TempData);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return list;
        }

        private string UpdateByUserIDCmd()
        {
            return @"
                    UPDATE UserStatusTable
                            SET [QID] = @QID
                                ,[OID] = @OID
                                ,[TempData] = @TempData
                    WHERE UserID = @UserID;
                    ";
        }
        #endregion


        #region 以UserID取得QID和OID
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
                            UserStatus userStatus = new UserStatus();
                            this.QID = reader.GetInt32(1);
                            this.OID = reader.GetInt32(2);
                            this.TempData = reader.GetString(3);
                        }
                    }
                }
            }
        }

        private string SelectByUserIDCmd()
        {
            return @"
                    SELECT [UserID]
                          ,[QID]
                          ,[OID]
                          ,[TempData]
                    FROM [dbo].[UserStatusTable]
                    WHERE UserID = @UserID; 
                    ";
        }
        #endregion


        #region 動作結束時，把使用者狀態初始化，依照UserID把QID, OID, tempData改成0
        internal void InitializeUserStatusByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InitializeUserStatusByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string InitializeUserStatusByUserIDCmd()
        {
            return @"
                     UPDATE UserStatusTable
                            SET [QID] = 0
                                ,[OID] = 0
                                ,[TempData] = '0'
                     WHERE UserID = @UserID;
                    ";
        }


        #endregion

        #region 任意時候想要紀錄暫存資訊可以先放在UserStatus的TempData裡面，這個方法不會動到QID和OID
        internal void UpdateTempDataByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateTempDataByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@TempData", this.TempData);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateTempDataByUserIDCmd()
        {
            return @"
                     UPDATE UserStatusTable
                            SET 
                                [TempData] = @TempData
                     WHERE UserID = @UserID;
                    ";
        }
        #endregion


        internal void SelectDisplayNameByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectDisplayNameByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserStatus userStatus = new UserStatus();

                            this.UserDisplayName = reader.GetString(0);
                        }
                    }
                }
            }
        }

        private string SelectDisplayNameByUserIDCmd()
        {
            return @"
SELECT 
      [UserDisplayName]
  FROM [Linebot].[dbo].[UserInfoTable]
  WHERE [UserID] = @UserID;";
        }
    }
}
