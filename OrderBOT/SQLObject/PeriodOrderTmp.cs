using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class PeriodOrderTmp
    {
        private string UserID;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
        public string ClubID { get; set; }
        public PeriodOrderTmp(string userId)
        {
            this.UserID = userId;
            this.StartDate = Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value);
            this.EndDate = Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value);
            this.StartTime = new TimeSpan(); 
            this.EndTime = new TimeSpan();
        }

        public PeriodOrderTmp()
        {
        }


        internal void UpdateStartDate()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateStartDateCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@StartDate", this.StartDate);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

        }
        private string UpdateStartDateCmd()
        {
            return @"
   UPDATE [dbo].[PeriodOrderTempTable]
   SET 
      [StartDate] = @StartDate
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

        private string UpdateStartTimeCmd()
        {
            return @"
   UPDATE [dbo].[PeriodOrderTempTable]
   SET 
      [StartTime] = @StartTime
   WHERE [UserID] = @UserID
";
        }

//        public void SelectAllByUserID()
//        {

//            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(SelectByUserIDCmd()))
//                {
//                    connection.Open();
//                    cmd.Connection = connection;
//                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            // OrderTmp orderTmp = new OrderTmp();
//                            this.StartDate = reader.GetDateTime(1);
//                            this.EndDate = reader.GetDateTime(2);
//                            //this.StartTime = reader.GetDateTime(3);
//                            this.StartTime = reader.GetTimeSpan(3);
//                            this.EndTime = reader.GetTimeSpan(4);
//                            //this.EndTime = Convert.ToDateTime(reader.GetString(2));
//                        }
//                    }
//                }
//            }
//        }

//        private string SelectByUserIDCmd()
//        {
//            return @"
//SELECT [UserID]
//      ,[StartDate]
//      ,[EndDate]
//      ,[StartTime]
//      ,[EndTime]
//  FROM [Linebot].[dbo].[PeriodOrderTempTable]
//  WHERE UserID = @UserID;
//";

//        }

        public void SelectAllByUserID()
        {

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SelectAllByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.StartDate = reader.GetDateTime(1);
                            this.EndDate = reader.GetDateTime(2);
                            this.Monday = reader.GetString(3);
                            this.Tuesday = reader.GetString(4);
                            this.Wednesday = reader.GetString(5);
                            this.Thursday = reader.GetString(6);
                            this.Friday = reader.GetString(7);
                            this.Saturday = reader.GetString(8);
                            this.Sunday = reader.GetString(9);
                            this.StartTime = reader.GetTimeSpan(10);
                            this.EndTime = reader.GetTimeSpan(11);
                            this.ClubID = reader.GetString(12);
                        }
                    }
                }
            }
        }

        private string SelectAllByUserIDCmd()
        {
            return @"
SELECT [UserID]
      ,[StartDate]
      ,[EndDate]
      ,[Monday]
      ,[Tuesday]
      ,[Wednesday]
      ,[Thursday]
      ,[Friday]
      ,[Saturday]
      ,[Sunday]
      ,[StartTime]
      ,[EndTime]
      ,[ClubID]
  FROM [Linebot].[dbo].[PeriodOrderTempTable]
  WHERE UserID = @UserID;
";

        }

        internal void ClubIDSelectByUserID()
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

        private string ClubIDSelectByUserIDCmd()
        {
            return @"
SELECT [ClubID]
  FROM [Linebot].[dbo].[PeriodOrderTempTable]
  WHERE UserID = @UserID;
";
        }

        internal void UpdateEndDate()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateEndDateCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@EndDate", this.EndDate);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

        }

        private string UpdateEndDateCmd()
        {
            return @"
   UPDATE [dbo].[PeriodOrderTempTable]
   SET 
      [EndDate] = @EndDate
   WHERE [UserID] = @UserID
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
   UPDATE [dbo].[PeriodOrderTempTable]
   SET 
      [EndTime] = @EndTime
   WHERE [UserID] = @UserID
";
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
                    cmd.Parameters.AddWithValue("@StartDate", this.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", this.EndDate);
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
UPDATE [dbo].[PeriodOrderTempTable]
   SET 
        [StartDate] = @StartDate
       ,[EndDate] = @EndDate
       ,[StartTime] = @StartTime
      ,[EndTime] = @EndTime
      ,[ClubID] = 'NoData'
 WHERE UserID = @UserID
";
        }

        internal bool CheckBothDateHasFilledIn(PeriodOrderTmp periodOrderTmp)
        {
            periodOrderTmp.SelectAllByUserID();
            DateTime SQLInitialTime = Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value);
            if (periodOrderTmp.StartDate == SQLInitialTime || periodOrderTmp.EndDate == SQLInitialTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal bool CheckBothTimeHasFilledIn(PeriodOrderTmp periodOrderTmp)
        {
            periodOrderTmp.SelectAllByUserID();
            TimeSpan SQLInitialTime = new TimeSpan ();
            if (periodOrderTmp.StartTime == SQLInitialTime || periodOrderTmp.EndTime == SQLInitialTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        internal void InsertInitialPeriodOrderTmp()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(InsertInitialPeriodOrderTmpCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@StartDate", this.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", this.EndDate);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }

        }



        private string InsertInitialPeriodOrderTmpCmd()
        {
            return @"
INSERT INTO [dbo].[PeriodOrderTempTable]
           ([UserID]
           ,[StartDate]
           ,[EndDate]
           ,[StartTime]
           ,[EndTime]
           )
     VALUES
           (@UserID
           ,@StartDate
           ,@EndDate
           ,@StartTime
           ,@EndTime
           )
";
        }

        internal void UpdateInitialPeriodOrderTmp()
        {

            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateInitialPeriodOrderTmpCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@StartDate", this.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", this.EndDate);
                    cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", this.EndTime);
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string UpdateInitialPeriodOrderTmpCmd()
        {
            return @"
UPDATE [dbo].[PeriodOrderTempTable]
   SET 
       [StartDate] = @StartDate
      ,[EndDate] = @EndDate
      ,[Monday] = 'N'
      ,[Tuesday] = 'N'
      ,[Wednesday] = 'N'
      ,[Thursday] = 'N'
      ,[Friday] = 'N'
      ,[Saturday] = 'N'
      ,[Sunday] = 'N'
      ,[StartTime] = @StartTime
      ,[EndTime] = @EndTime
      ,[ClubID]= 'NoData'
 WHERE UserID = @UserID
";
        }
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
UPDATE [dbo].[PeriodOrderTempTable]
   SET 
      [ClubID] = @ClubID
 WHERE UserID = @UserID;
";
            throw new NotImplementedException();
        }

        internal void WeekofDaySelectByUserID()
        {
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(WeekofDaySelectByUserIDCmd()))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.Monday = reader.GetString(0);
                            this.Tuesday = reader.GetString(1);
                            this.Wednesday = reader.GetString(2);
                            this.Thursday = reader.GetString(3);
                            this.Friday = reader.GetString(4);
                            this.Saturday = reader.GetString(5);
                            this.Sunday = reader.GetString(6);
                        }
                    }
                }
            }
        }

        private string WeekofDaySelectByUserIDCmd()
        {
            return @"
SELECT [Monday]
      ,[Tuesday]
      ,[Wednesday]
      ,[Thursday]
      ,[Friday]
      ,[Saturday]
      ,[Sunday]
  FROM [Linebot].[dbo].[PeriodOrderTempTable]
  WHERE UserID = @UserID;
";

        }

        internal void UpdateWeekofDayToNo(string WeekoDay)
        {
            string UpdateWeekofDayToNoCmd = GetUpdateWeekofDayToNoCmd(WeekoDay);
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateWeekofDayToNoCmd))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    switch (WeekoDay)
                    {
                        case "1":
                            cmd.Parameters.AddWithValue("@Monday", this.Monday);
                            break;
                        case "2":
                            cmd.Parameters.AddWithValue("@Tuesday", this.Tuesday);
                            break;
                        case "3":
                            cmd.Parameters.AddWithValue("@Wednesday", this.Wednesday);
                            break;
                        case "4":
                            cmd.Parameters.AddWithValue("@Thursday", this.Thursday);
                            break;
                        case "5":
                            cmd.Parameters.AddWithValue("@Friday", this.Friday);
                            break;
                        case "6":
                            cmd.Parameters.AddWithValue("@Monday", this.Saturday);
                            break;
                        case "7":
                            cmd.Parameters.AddWithValue("@Sunday", this.Sunday);
                            break;
                        default:
                            break;
                    }
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        private string GetUpdateWeekofDayToNoCmd(string weekoDay)
        {
            string Day = "";
            switch (weekoDay)
            {
                case "1":
                    Day = "[Monday]";
                    break;

                case "2":
                    Day = "[Tuesday]";
                    break;

                case "3":
                    Day = "[Wednesday]";
                    break;

                case "4":
                    Day = "[Thursday]";
                    break;

                case "5":
                    Day = "[Friday]";
                    break;

                case "6":
                    Day = "[Saturday]";
                    break;

                case "7":
                    Day = "[Sunday]";

                    break;
                default:
                    break;
            }


            return $@"
UPDATE [dbo].[PeriodOrderTempTable]
   SET {Day} = 'N'
  WHERE UserID = @UserID;
";
        }



        internal void UpdateWeekofDayToYes(string WeekoDay)
        {
            string UpdateWeekofDayToYesCmd = GetUpdateWeekofDayToYesCmd(WeekoDay);
            using (SqlConnection connection = new SqlConnection(DBHelper.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UpdateWeekofDayToYesCmd))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    switch (WeekoDay)
                    {
                        case "1":
                            cmd.Parameters.AddWithValue("@Monday", this.Monday);
                            break;
                        case "2":
                            cmd.Parameters.AddWithValue("@Tuesday", this.Tuesday);
                            break;
                        case "3":
                            cmd.Parameters.AddWithValue("@Wednesday", this.Wednesday);
                            break;
                        case "4":
                            cmd.Parameters.AddWithValue("@Thursday", this.Thursday);
                            break;
                        case "5":
                            cmd.Parameters.AddWithValue("@Friday", this.Friday);
                            break;
                        case "6":
                            cmd.Parameters.AddWithValue("@Monday", this.Saturday);
                            break;
                        case "7":
                            cmd.Parameters.AddWithValue("@Sunday", this.Sunday);
                            break;
                        default:
                            break;
                    }
                    DBHelper dBHelper = new DBHelper();
                    dBHelper.ExecuteNonQuery(cmd);
                }
            }
        }



        private string GetUpdateWeekofDayToYesCmd(string weekoDay)
        {
            string Day = "";
            switch (weekoDay)
            {
                case "1":
                    Day = "[Monday]";
                    break;

                case "2":
                    Day = "[Tuesday]";
                    break;

                case "3":
                    Day = "[Wednesday]";
                    break;

                case "4":
                    Day = "[Thursday]";
                    break;

                case "5":
                    Day = "[Friday]";
                    break;

                case "6":
                    Day = "[Saturday]";
                    break;

                case "7":
                    Day = "[Sunday]";

                    break;
                default:
                    break;
            }


            return $@"
UPDATE [dbo].[PeriodOrderTempTable]
   SET {Day} = 'Y'
  WHERE UserID = @UserID;
";
        }
    }

}