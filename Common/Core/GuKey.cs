using System;

namespace Common.Core
{
    /// <summary>
    /// 时间排序GUID 兼容Sql GUID
    /// </summary>
    public static class GuKey
    {
        /// <summary>
        /// 基准日期
        /// </summary>
        public static DateTime BaseDate = new DateTime(1970, 1, 1);

        /// <summary>
        /// 时间排序GUID 兼容Sql GUID
        /// </summary>
        /// <returns></returns>
        public static Guid NewId()
        {
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();
            DateTime now = DateTime.Now;

            TimeSpan days = new TimeSpan(now.Ticks - BaseDate.Ticks);
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }
        /// <summary>
        /// 从Guid上取出时间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DateTime GetDate(Guid id)
        {
            byte[] guidArray = id.ToByteArray();
            byte[] daysArray = new byte[4];
            byte[] msecsArray = new byte[4];

            // 拷贝出时间信息
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);

            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // 转成天数和毫秒
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);
            //合成日期
            DateTime date = BaseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);
            return date;
        }

    }
}