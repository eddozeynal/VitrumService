using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPService
{
    public static class ExtensionMethods
    {
        public static DateTime GetDateFromFormattedString(this string FormattedString)
        {
            int year = Convert.ToInt32(FormattedString.Substring(0, 4));
            int month = Convert.ToInt32(FormattedString.Substring(4, 2));
            int day = Convert.ToInt32(FormattedString.Substring(6, 2));
            int hour = Convert.ToInt32(FormattedString.Substring(8, 2));
            int minute = Convert.ToInt32(FormattedString.Substring(10, 2));
            int second = Convert.ToInt32(FormattedString.Substring(12, 2));
            return new DateTime(year, month, day, hour, minute, second);
        }
        public static string GetFormattedStringFromDate(this DateTime Date)
        {
            string retval = "";
            retval += Date.Year.ToString();
            if (Date.Month < 10) retval += "0";
            retval += Date.Month.ToString();
            if (Date.Day < 10) retval += "0";
            retval += Date.Day.ToString();
            if (Date.Hour < 10) retval += "0";
            retval += Date.Hour.ToString();
            if (Date.Minute < 10) retval += "0";
            retval += Date.Minute.ToString();
            if (Date.Second < 10) retval += "0";
            retval += Date.Second.ToString();
            return retval;
        }

        public static Tuple<bool, T, string> ToTuple<T>(Operation<T> operation)
        {
            return new Tuple<bool, T, string>(operation.Successful, operation.Value, operation.Fail);
        }
        public static T GetEligibleOjbect<T>(this object Value)
        {
            string SerializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(Value);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(SerializedObject);
        }
    }
}