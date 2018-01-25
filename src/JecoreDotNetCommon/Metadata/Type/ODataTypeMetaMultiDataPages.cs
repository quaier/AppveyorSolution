using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Metadata.Type
{
    public class ODataTypeMetaMultiDataPages<TSource1, TSource2, TSource3, TPage>
        where TSource1 : class
        where TSource2 : class
        where TSource3 : class
        where TPage : class
    {
        public TPage Page { private set; get; }

        public TSource1 Array1 { private set; get; }

        public TSource2 Array2 { private set; get; }

        public TSource3 Array3 { private set; get; }

        public ODataTypeMetaMultiDataPages(TSource1 source, TSource2 source2, TSource3 source3, TPage paging)
        {
            this.Array1 = source;
            this.Array2 = source2;
            this.Array3 = source3;
            this.Page = paging;
        }
    }
}
