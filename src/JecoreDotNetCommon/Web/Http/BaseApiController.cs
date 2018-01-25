using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JecoreDotNetCommon.Web.Http
{
    public class BaseApiController : ApiController
    {
        // GET: BaseApi

        public string Userid
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var user = identity.Claims.Where(p => p.Type == "sub").SingleOrDefault();
                if (user == null)
                {
                    return null;
                }
                return user.Value;
            }
        }

        /// <summary>
        /// 获取当前请求携带的 Authorization
        /// </summary>
        /// <returns></returns>
        protected string GetToken()
        {
            return GetDataFromValueProvider(ActionContext, "Authorization");
        }

        private string GetDataFromValueProvider(HttpActionContext filterContext, string key)
        {
            if (filterContext != null)
            {
                IEnumerable<string> tokenHeaders;
                bool bToken = filterContext.Request.Headers.TryGetValues(key, out tokenHeaders);
                if (bToken)
                {
                    foreach (var tokenHeader in tokenHeaders)
                    {
                        return tokenHeader;
                    }
                }
            }
            return null;
        }
    }
}