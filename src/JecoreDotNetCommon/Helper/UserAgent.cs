using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JecoreDotNetCommon.Helper
{
    public static class UserAgent
    {
        /// <summary>
        /// 判断是否是微信浏览器
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static bool IsWexin(HttpRequestBase httpRequest)
        {
            string userAgent = httpRequest.UserAgent;
            if (userAgent.ToLower().Contains("micromessenger"))
            {
                return true;
            }
            return false;
        }
    }
}
