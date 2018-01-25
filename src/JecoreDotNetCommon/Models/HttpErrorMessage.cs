using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Models
{
    /// <summary>
    /// api 错误信息类
    /// </summary>
    public class HttpErrorMessage
    {
        public string Message { set; get; }
        public bool Error { set; get; }
    }
}
