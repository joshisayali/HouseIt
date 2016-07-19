using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHoldManagement.Utilities
{
    public static class ApplicationDateTime
    {
        public static DateTime DefaultDateTime
        {
            get
            {
                return default(DateTime);
            }

        }

        public static DateTime FirstDayOfMonth
        {
            get
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

        }

        public static DateTime LastDayOfMonth
        {
            get
            {
                DateTime firstDay = FirstDayOfMonth;
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, firstDay.AddMonths(1).AddDays(-1).Day);
            }

        }

        public static DateTime MinDate
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime MaxDate
        {
            get
            {
                return DateTime.MaxValue;
            }
        }
    }
}
