using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JecoreDotNetCommon.Metadata.Type
{
    //public class ODataMetadata 
    //{
    //    private readonly long? _count;
    //    private IEnumerable<object> _result;

    //    public ODataMetadata(IEnumerable<object> result, long? count)
    //    {
    //        _count = count;
    //        _result = result;
    //    }

    //    public IEnumerable<object> Results
    //    {
    //        get { return _result; }
    //    }

    //    public long? Count
    //    {
    //        get { return _count; }
    //    }
    //}
    public class ODataMetadata:IODataMetadata
    {
        private readonly long? _count;
        private object _result;

        public ODataMetadata(object result, long? count)
        {
            _count = count;
            _result = result;
        }

        public object Results
        {
            get { return _result; }
        }

        public long? Count
        {
            get { return _count; }
        }
    }
    }
