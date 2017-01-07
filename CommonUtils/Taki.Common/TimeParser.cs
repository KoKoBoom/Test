using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taki.Common
{
    public class TimeParser
    {
        #region 把秒转换成分钟
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }
        #endregion

        #region 获取指定月份第一天的日期
        /// <summary>
        /// 获取指定月份第一天的日期
        /// </summary>
        /// <param name="year">年份 默认当前年</param>
        /// <param name="month">月份 默认当前月</param>
        /// <returns>DateTime</returns>
        public static DateTime GetMonthFirstDate(int year, int month)
        {
            var currentDate = DateTime.Now;
            if (year == 0) year = currentDate.Year;
            if (month == 0) month = currentDate.Month;
            return new DateTime(year, month, 1);
        }
        /// <summary>
        /// 获取指定月份第一天的日期
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstDate(int month) => GetMonthFirstDate(0, month);
        /// <summary>
        /// 获取指定月份第一天的日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstDate(DateTime dateTime) => GetMonthFirstDate(dateTime.Year, dateTime.Month);
        /// <summary>
        /// 获取指定月份第一天的日期
        /// </summary>
        /// <param name="year">年份 默认当前年</param>
        /// <param name="month">月份 默认当前月</param>
        /// <param name="format">日期格式 默认 YYYY-MM-dd HH:mm:ss </param>
        /// <returns> string 类型的 DateTime </returns>
        public static string GetMonthFirstDate(int year, int month, string format = "YYYY-MM-dd HH:mm:ss") => GetMonthFirstDate(year, month).ToString(format);
        #endregion

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

    }
}
