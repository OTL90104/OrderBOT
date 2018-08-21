using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderBot.Utility
{
    public class PeriodSplitter
    {
        public static List<DateTime> Cut(DateTime statTime,DateTime endTime,List<string> selectedDays)
        {
            List<DateTime> list = new List<DateTime>();
          int result=  DateTime.Compare(statTime, DateTime.Now);
            //起始時間若是超過目前時間以目前時間為準
            if (result<0)
            {
                statTime = DateTime.Now;
            }


            for (DateTime dt = statTime; dt <= endTime; dt = dt.AddDays(1))
            {
                foreach (string item in selectedDays)
                {
                    
                    if (dt.DayOfWeek.ToString()==item)
                    {
                        list.Add(dt);
                    }
                }

            }

            return list;
        }
    }
}