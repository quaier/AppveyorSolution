using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace JecoreDotNetCommon.Web.Http
{
    /// <summary>
    /// Token 授权验证
    /// </summary>
    public class AuthorizeByTokenAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext httpActionContext)
        {
            if (!CheckIsSignatureValid(httpActionContext))
            {
                HandleUnauthorizedRequest(httpActionContext);
            }

            //base.OnAuthorization(httpActionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext httpActionContext)
        {
            var stringContent = new StringContent(
                    JsonConvert.SerializeObject(new { Message = "请求的令牌无效" }));

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            response.Content = stringContent;

            response.Headers.Add("Authorization", "The request of the token is invalid");

            throw new HttpResponseException(response);
        }

        private bool CheckIsSignatureValid(HttpActionContext filterContext)
        {
            string token = GetDataFromValueProvider(filterContext, "Authorization");

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            else
            {
                filterContext.ActionArguments["token"] = token;
                return true;
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