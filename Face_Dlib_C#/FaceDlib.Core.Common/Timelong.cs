using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Common
{
    public class Timelong
    {
        /// <summary>
        /// 获取当前本地时间戳
        /// </summary>
        /// <returns></returns>      
        public static long GetCurrentTimeUnix()
        {
            TimeSpan cha = DateTime.Now - TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long t = (long)cha.TotalSeconds;
            return t;
        }
        /// <summary>
        /// 时间戳转换为本地时间对象
        /// </summary>
        /// <returns></returns>      
        public static DateTime GetUnixDateTime(long unix)
        {
            //long unix = 1500863191;
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            DateTime newTime = dtStart.AddSeconds(unix);
            return newTime;
        }
    }
}
