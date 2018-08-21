using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class ClubInfo
    {
        internal string UserID{ get; set; }
        internal string ClubName{ get; set; }
        internal string ClubID{ get; set; }
        public ClubInfo(string userid ,string clubname)
        {
            this.UserID = userid;
            this.ClubName = $"{clubname}";
            this.ClubID = "C"+DateTime.Now.ToString("yyyyMMddHHmmssfff")+userid;
        }

        public ClubInfo(string userid, string clubID,string tmp)
        {
            this.UserID = userid;
            this.ClubID = clubID;
        }

        public ClubInfo()
        {
        }

        public ClubInfo(string userId)
        {
            UserID = userId;
        }

        #region Delete
        public List<ClubInfo> DeleteClubInfoToSQL()
        {
            List<ClubInfo> list = new List<ClubInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DeleteClubUserTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
            return list;
        }
        private string DeleteClubUserTableCmd()
        {
            return @"
                    UPDATE [dbo].[ClubUserTable]
                       SET [isDelete] = 'Y'
                    WHERE UserID = @UserID and ClubID = @ClubID
                    ";
        }
        #endregion
        
        public List<ClubInfo> InsertClubInfoToSQL()
        {
            List<ClubInfo> list = new List<ClubInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertClubUserTableCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
            using (SqlConnection connection2 = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertClubNameTableCmd()))
                {
                    connection2.Open();
                    cmd.Connection = connection2;
                    cmd.Parameters.AddWithValue("@Clubname", this.ClubName);
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubID);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }           
            return list;
        }
        private string InsertClubUserTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[ClubUserTable]
                   ([UserID]
                   ,[ClubID]
                   ,[isDelete]
                    ,[isAdmin]
)
                   VALUES
                  (@UserID
                  ,@ClubID
                  ,'N'
                  ,'Y')
                    ";
        }
        private string InsertClubNameTableCmd()
        {
            return @"
                    INSERT INTO [dbo].[ClubNameTable]
                    ([ClubID]
                   ,[ClubName])
                   VALUES
                   (@ClubID
                   ,@ClubName)
                    ";
        }
                
        public List<ClubInfo> SelectByUserid()
        {
            List<ClubInfo> list = new List<ClubInfo>();

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
                            ClubInfo clubInfo = new ClubInfo();
                            clubInfo.UserID = reader.GetString(0);
                            clubInfo.ClubID = reader.GetString(1);
                            clubInfo.ClubName = reader.GetString(2);
                            list.Add(clubInfo);
                        }
                    }
                }
            }
            return list;
        }
        private string SelectByUseridcmd()
        {
            return @"
                      SELECT
                           C.UserID
                          ,C.ClubID
                    	  ,CN1.ClubName
                      FROM [Linebot].[dbo].[ClubUserTable] AS C
                      LEFT JOIN
                      (
                      SELECT 
                      CN.ClubID,
                      CN.ClubName
                      FROM [Linebot].[dbo].[ClubNameTable] AS CN
                      
                      )CN1
                      ON CN1.ClubID = C.ClubID
                      WhERE C.UserID = @UserID and C.isDelete = 'N'
                    ";
        }

        public List<ClubInfo> SelectByClubid()
        {
            List<ClubInfo> list = new List<ClubInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByClubidcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClubInfo clubInfo = new ClubInfo();
                            clubInfo.ClubID = reader.GetString(0);
                            clubInfo.ClubName = reader.GetString(1);
                            list.Add(clubInfo);
                        }
                    }
                }
            }
            return list;
        }
        private string SelectByClubidcmd()
        {
            return @"
                    SELECT [ClubID]
                          ,[ClubName]
                    FROM [dbo].[ClubNameTable]
                    WHERE ClubID = @ClubID;";
        }
        
        public List<ClubInfo> SelectByUseridandClubID()
        {
            List<ClubInfo> list = new List<ClubInfo>();

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectByUseridandClubIDcmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ClubID", this.ClubID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClubInfo clubInfo = new ClubInfo();
                            clubInfo.UserID = reader.GetString(0);
                            clubInfo.ClubID = reader.GetString(1);
                            clubInfo.ClubName = reader.GetString(2);
                            list.Add(clubInfo);
                        }
                    }
                }
            }
            return list;
        }
        private string SelectByUseridandClubIDcmd()
        {
            return @"
                    SELECT
                           C.UserID
                          ,C.ClubID
                    	  ,CN1.ClubName
                    FROM [Linebot].[dbo].[ClubUserTable] AS C
                    LEFT JOIN
                    (
                    SELECT 
                    CN.ClubID,
                    CN.ClubName
                    FROM [Linebot].[dbo].[ClubNameTable] AS CN
                    
                    )CN1
                    ON CN1.ClubID = C.ClubID
                    WhERE C.UserID = @UserID and C.ClubID = @ClubID
                    ";
        }
        
    }
}