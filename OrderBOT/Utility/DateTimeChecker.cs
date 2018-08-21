using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    /// <summary>
    /// 檢查當下時間是否超過傳入的時間字串有效時間(1)分鐘
    /// </summary>
    /// true:有效時間
    /// false:時間愈期
    public static class DateTimeChecker
    {
        public static bool TimeCheck(string DateTimeString)
        {
            DateTime btndateTime = Convert.ToDateTime(DateTimeString);
            DateTime enddateTime = btndateTime.AddMinutes(5);
            DateTime userdateTime = DateTime.Now;
            int result = DateTime.Compare(userdateTime, enddateTime);
            if (result<0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool DateTimeCheckIsEarlierThanNowForPeriod(DateTime InsertDateTime)
        {

            int result = DateTime.Compare(InsertDateTime, DateTime.Now.Date);
            if (result < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool DateTimeCheckIsEarlierThanNow(DateTime InsertDateTime)
        {

            int result = DateTime.Compare(InsertDateTime, DateTime.Now.AddMinutes(-1));
            if (result < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CompareIsLaterThanEndTime(DateTime StartTime, OrderTemp orderTmp)
        {
            orderTmp.SelectByUserID();
            if (orderTmp.EndTime == Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value))
            {
                return false;
            }
            int result = DateTime.Compare(StartTime, orderTmp.EndTime);
            if (result < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool DateTimeCompareIsLaterThanEndTime(DateTime StartDate ,PeriodOrderTmp periodOrderTmp)
        {
            periodOrderTmp.SelectAllByUserID();
            if (periodOrderTmp.EndDate== Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value))
            {
                return false;
            }
            int result = DateTime.Compare(StartDate, periodOrderTmp.EndDate);
            if (result < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CompareIsEarlierThanStartTime(DateTime EndTime, OrderTemp orderTmp)
        {
            orderTmp.SelectByUserID();
            if (orderTmp.StartTime == Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value))
            {
                return false;
            }
            int result = DateTime.Compare(orderTmp.StartTime, EndTime);
            if (result < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool DateTimeCompareIsEarlierThanStartTime(DateTime EndDate, PeriodOrderTmp periodOrderTmp)
        {
            periodOrderTmp.SelectAllByUserID();
            if (periodOrderTmp.StartDate == Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value))
            {
                return false;
            }
            int result = DateTime.Compare(periodOrderTmp.StartDate, EndDate);
            if (result < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool TimeCompareIsLaterThanEndTime(TimeSpan StartTime, PeriodOrderTmp periodOrderTmp)
        {
            periodOrderTmp.SelectAllByUserID();
            if (periodOrderTmp.EndTime == new TimeSpan())
            {
                return false;
            }
            int result = TimeSpan.Compare(StartTime, periodOrderTmp.EndTime);
            //int result = DateTime.Compare(StartTime, periodOrderTmp.EndTime);
            if (result > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal static bool TimeCompareIsEarlierThanStartTime(TimeSpan EndTime, PeriodOrderTmp periodOrderTmp)
        {
            periodOrderTmp.SelectAllByUserID();
            if (periodOrderTmp.StartTime == new TimeSpan())
            {
                return false;
            }
            int result = TimeSpan.Compare(EndTime, periodOrderTmp.StartTime);
//            int result = DateTime.Compare(periodOrderTmp.StartTime, EndTime);
            if (result > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}