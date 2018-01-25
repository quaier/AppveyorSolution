using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Metadata.Type
{
    /// <summary>
    /// 通过 静态泛型方法 简化 泛型类型的创建
    /// 让编译器 隐式类型推断
    /// </summary>
    public static class TypeMetadata
    {
        public static ODataTypeMetadata<TResults> Of<TResults>(TResults result, long? count = null) where TResults : class
        {
            return new ODataTypeMetadata<TResults>(result, count);
        }
    }

    /// <summary>
    /// 泛型接口数据源包装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ODataTypeMetadata<TResults> : IODataTypeMetadata<TResults>
        where TResults:class
    {
        /// <summary>
        /// 数据源包装类
        /// </summary>
        /// <param name="result">数据源</param>
        /// <param name="count">记录总数 不填默认值为空</param>
        public ODataTypeMetadata(TResults result, long? count = null)
        {
            Count = count;
            Results = result;
        }
        /// <summary>
        /// 泛型 数据源
        /// </summary>
        public TResults Results {  set; get; }
        /// <summary>
        /// 数据源记录总数
        /// </summary>
        public long? Count {  set; get; }
        
    }
}
