using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Newtonsoft.Json;

namespace JecoreDotNetCommon.Web.Http
{
    /// <summary>
    /// 允许拥有RequestVerificationToken令牌的匿名请求访问
    /// </summary>
    public class AllowAnonymousByTokenAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            string token = GetDataFromValueProvider(filterContext, "RequestVerificationToken");
            CheckIsSignatureValid(token, filterContext);

        }

        private void CheckIsSignatureValid(string token, HttpActionContext filterContext)
        {
            string stoken = ConfigurationManager.AppSettings["RequestVerificationToken"];
            if (token != stoken)
            {
                var challengeMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                challengeMessage.Headers.Add("RequestVerificationToken", "Unauthorized request");
                var str = JsonConvert.SerializeObject(new { Message = "未授权的请求" });
                challengeMessage.Content = new StringContent(str);
                filterContext.Response = challengeMessage;
            }
        }

        private string GetDataFromValueProvider(HttpActionContext filterContext, string key)
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
            return null;
        }
    }
}
