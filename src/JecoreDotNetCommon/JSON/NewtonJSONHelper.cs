using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.JSON
{
    /// <summary>
    /// NewtonJSON 拓展
    /// </summary>
    public static class NewtonJSONHelper
    {
        /// <summary>
        /// 序列化为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(this object obj)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(obj, Formatting.Indented, timeFormat);
        }

        /// <summary>
        /// 反序列化为指定的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        /// <summary>
        /// 反序列化为指定的匿名对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T DeserializeAnonymousType<T>(this string data, T t)
        {
            return JsonConvert.DeserializeAnonymousType(data, t, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
