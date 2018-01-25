using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Metadata.Type
{
    public interface IODataMetadata
    {
        long? Count { get; }
        object Results { get; }

    }
}
