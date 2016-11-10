using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ExtendMethod
    {/// <summary>
     /// 8位字符串转换成日期
     /// </summary>
     /// <param name="dt"></param>
     /// <returns></returns>
        public static DateTime ToDate(this string dt)
        {
            return DateTime.ParseExact(dt, "yyyyMMdd", new CultureInfo("zh-CN"), DateTimeStyles.AllowWhiteSpaces);
        }
        public static DateTime ToDateTime(this string dt)
        {
            return DateTime.ParseExact(dt, "yyyyMMddHHmmss", new CultureInfo("zh-CN"), DateTimeStyles.AllowWhiteSpaces);
        }

        public static string ToDateString(this string dt)
        {
            return DateTime.ParseExact(dt, "yyyyMMdd", new CultureInfo("zh-CN"), DateTimeStyles.AllowWhiteSpaces).ToDateString();
        }
        public static string ToDateTimeString(this string dt)
        {
            return DateTime.ParseExact(dt, "yyyyMMddHHmmss", new CultureInfo("zh-CN"), DateTimeStyles.AllowWhiteSpaces).ToDateTimeString();
        }
        public static string ToDateString(this DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd");
        }
        public static string ToDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd HH:mm:ss");
        }
        /// <summary>
        /// 获取字符串长度，中文占两位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetByte(this string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        public static string FullMessage(this Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;
            else
                return string.Format("{0} -> {1}", ex.Message, ex.InnerException.FullMessage());
        }

        public static string FullStackTrace(this Exception ex)
        {
            if (ex.InnerException == null)
                return ex.StackTrace;
            else
                return string.Format("{0} -> {1}", ex.StackTrace, ex.InnerException.FullStackTrace());
        }
        public static string JsonSerial(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T JsonDeserial<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
