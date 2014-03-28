using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularPlanner.Helpers
{
    public class TimeSpanHelper
    {
        public static DateBetween GetTimeSpan(string date)
        {
            var arr = date.Split('-');

            if (arr.Count() == 1)
            {
                return new DateBetween
                {
                    Lower = new DateTime(int.Parse(arr[0]), 1, 1, 0, 0, 0),
                    Higher = new DateTime(int.Parse(arr[0]), 12, 31, 23, 59, 59)
                };
            }
            if (arr.Count() == 2)
            {
                return new DateBetween
                {
                    Lower = new DateTime(int.Parse(arr[1]), int.Parse(arr[0]), 1, 0, 0, 0),
                    Higher = new DateTime(int.Parse(arr[1]), int.Parse(arr[0]), DateTime.DaysInMonth(int.Parse(arr[1]), int.Parse(arr[0])), 23, 59, 59)
                };
            }
            return new DateBetween
            {
                Lower = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]), 0, 0, 0),
                Higher = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]), 23, 59, 59)
            };
        }
    }

    public class DateBetween
    {
        public DateTime Lower { get; set; }
        public DateTime Higher { get; set; }
    }
}