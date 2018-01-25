using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Helper
{
    /// <summary>
    /// Enum帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举成员的值(this是扩展方法的标志)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }

        public static T ToEnum<T>(this string obj) where T : struct
        {
            if (string.IsNullOrEmpty(obj))
            {
                return default(T);
            }
            try
            {
                return (T)Enum.Parse(typeof(T), obj, true);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 获取指定枚举成员的描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum obj)
        {
            if (obj != null)
            {
                var type_ = obj.GetType();
                var field_ = type_.GetField(obj.ToString());
                if (field_ != null)
                {
                    var attribs = (DescriptionAttribute[])field_.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    return attribs.Length > 0 ? attribs[0].Description : obj.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 根据枚举值，获取指定枚举类的成员描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetDescription(this Type type, int? id)
        {
            var values = from Enum e in Enum.GetValues(type)
                         select new { id = e.ToInt(), name = e.ToDescription() };

            if (!id.HasValue) id = 0;

            try
            {
                return values.ToList().Find(c => c.id == id).name;
            }
            catch
            {
                return "";
            }
        }
    }
}
