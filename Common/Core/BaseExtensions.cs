using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace Common.Core
{
    /// <summary>
    /// string 扩展
    /// </summary>
    public static class StrExtensions
    {
        #region null 或空白字符串
        /// <summary>
        /// null 或 空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) || s.IsNullOrBlank();
        }

        /// <summary>
        /// null 或空白字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrBlank(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        } 
        #endregion

        #region 是否为中文
        private static readonly Regex RxChiese = new Regex("^[\u4e00-\u9fa5]$", RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly Regex RxSymbol = new Regex(@"[，。；？~！：‘“”’【】（）]", RegexOptions.Compiled | RegexOptions.Multiline);
        /// <summary>
        /// 是否为中文
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsChinese(this string s)
        {
            return !RxChiese.IsMatch(s);
        }
        #endregion

        #region json 序列化
        /// <summary>
        /// json 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        #endregion

        #region 反序列化json 字符串
        /// <summary>
        /// 反序列化json 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
        public static object FromJson(this string obj, Type type)
        {
            return JsonConvert.DeserializeObject(obj, type);
        }
        #endregion

        #region 忽略大小写比较字符串
        /// <summary>
        /// 忽略大小写比较字符串
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        public static bool IgnorCaseTo(this string strA, string compareTo)
        {
            return string.Compare(strA, compareTo, true) == 0;
        }
        #endregion

        #region 将字符串转为int
        /// <summary>
        /// 将字符串转为int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt32(this string str)
        {
            return Convert.ToInt32(str);
        }
        #endregion

        #region 将字符串转为decimal
        /// <summary>
        /// 将字符串转为decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal ToDecima(this string str)
        {
            return Convert.ToDecimal(str);
        }
        #endregion

        #region 将字符串转为datetime
        /// <summary>
        /// 将字符串转为datetime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            return Convert.ToDateTime(str);
        }
        #endregion

        #region 将字符串转为GUID
        /// <summary>
        /// 将字符串转为GUID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            return Guid.Parse(str);
        } 
        #endregion
    }
   
    /// <summary>
    /// 集合扩展（IEnumerable）
    /// </summary>
    public static class EnumerableExtensions
    {

        #region IEnumerable转为IEnumerable<object>
        /// <summary>
        /// IEnumerable转为IEnumerable<object>
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<object> ToEnumrableT(this IEnumerable list)
        {
            var en = list.GetEnumerator();
            while (en.MoveNext())
            {
                yield return en.Current;
            }
            en = null;
        }
        #endregion

        #region 根据下标获取集合数据
        /// <summary>
        /// 根据下标获取集合数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static object GetByIndex(this IEnumerable list, int idx)
        {
            var en = list.GetEnumerator();
            int i = 0;
            object r = null;
            while (en.MoveNext())
            {
                if (i == idx)
                {
                    r = en.Current;
                    break;
                }
                i++;
            }
            en = null;
            return r;
        } 
        #endregion

        #region 集合不为null并且有至少一个元素
        /// <summary>
        /// 集合不为null并且有至少一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasElement<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        } 
        #endregion

        #region 遍历集合，执行act
        /// <summary>
        /// 遍历集合，执行act
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="act">每一项上执行的方法</param>
        public static void Each<T>(this IEnumerable<T> list, Action<T> act)
        {
            using (var en = list.GetEnumerator())
            {
                while (en.MoveNext())
                    act(en.Current);
            }
        }
        #endregion

        #region 异步遍历集合数据
        public static Task WhenAllAsync<T>(this IEnumerable<T> values, Func<T, Task> asyncAction)
        {
            return Task.WhenAll(values.Select(asyncAction));
        }
        public static Task WhenAllAsync<T>(this IEnumerable<T> values, Func<T, int, Task> asyncAction)
        {
            return Task.WhenAll(values.Select(asyncAction));
        }
        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> values, Func<TSource, Task<TResult>> asyncSelector)
        {
            return await Task.WhenAll(values.Select(asyncSelector));
        }
        #endregion

        #region 连接字符串
        /// <summary>
        /// 连接字符串，非string类型会调用toString 进行连接，null=》string.Empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="separator">连接符 默认"，"</param>
        /// <param name="emptyModel">如何处理 空/0 长度字符串 false--不处理，true--处理（默认值）</param>
        /// <returns></returns>
        public static string JoinStr<T>(this IEnumerable<T> arr, string separator = ",", bool emptyModel = true)
        {

            return emptyModel ? (string.Join(separator, arr.Select(c => c + ""))) : (string.Join(separator, arr.Select(c => c + "").Where(c => c.Length > 0)));

        } 
        #endregion
    }
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        #region 取枚举名称
        /// <summary>
        /// 取枚举名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumvalue"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(this T enumvalue) where T : struct
        {
            return Enum.GetName(typeof(T), enumvalue);
        } 
        #endregion
    }

    /// <summary>
    /// 异常及日志扩展
    /// </summary>
    public static class ExceptLogExtensions
    {
        [ThreadStatic]
       private  static readonly Logger _loger = null;
        /// <summary>
        /// 线程唯一的日志对象
        /// </summary>
        public static Logger Loger { get { return _loger ?? LogManager.GetCurrentClassLogger(); } }
        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="name">名称（null=》ex.Message）</param>
        public static void LogError(this Exception ex, string name = null)
        {
            Loger.Error(ex.Message, ex,LogLevel.Error);
        }

        public static void LogMessage(string message, params object[] args)
        {
            Loger.Debug(message, args);
        }
    }
}