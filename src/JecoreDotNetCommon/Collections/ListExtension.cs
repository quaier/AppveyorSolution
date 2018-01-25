using System;
using System.Collections.Generic;
using System.Linq;

namespace JecoreDotNetCommon.Collections
{
    /// <summary>
    /// 返回指定页码的数据
    /// </summary>
    public static class ListExtension
    {
        public static List<T> Pager<T>(this IEnumerable<T> t, int id, int pageSize)
        {
            int startIndex = id > 1 ? (id - 1) * pageSize + 1 : 0;
            t.Skip(startIndex).Take(pageSize);
            return t.ToList<T>();
        }

        public static List<T> Pager<T>(this IOrderedEnumerable<T> t, int id, int pageSize)
        {
            int startIndex = id > 1 ? (id - 1) * pageSize + 1 : 0;
            t.Skip(startIndex).Take(pageSize);
            return t.ToList<T>();
        }
    }
}
