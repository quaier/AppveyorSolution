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
    public static class TypeMultiDataPages
    {
        public static ODataTypeMultiDataPages<TSource, TPage> Of<TSource,TPage >(TSource source,TPage page)
            where TSource:class 
            where TPage:class
        {
            return new ODataTypeMultiDataPages<TSource, TPage>(source, page);
        }
    }

    public interface IODataTypeMultiDataPages<TSource, TPage>
        where TSource : class
        where TPage : class
    {
         TPage Page {  get; }

         TSource Array {  get; }
    }

    /// <summary>
    /// 分页数据包装类
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TPage"></typeparam>
    public class ODataTypeMultiDataPages<TSource, TPage> : IODataTypeMultiDataPages<TSource, TPage>
        where TSource : class
        where TPage : class
    {
        public TPage Page { set; get; }

        public TSource Array { set; get; }

        public ODataTypeMultiDataPages(TSource source, TPage paging)
       {
           this.Array = source;
           this.Page = paging;
       }
    }


}
