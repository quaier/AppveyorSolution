using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using JecoreDotNetCommon.Models;
using JecoreDotNetCommon.Extensions;

namespace JecoreDotNetCommon.Web.Http
{
    /// <summary>
    /// WebApi Controller 异常处理
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext httpActionExecutedContext)
        {
            // 错误状态 默认为500
            int errorCode = 500;
            HttpException httpEx = httpActionExecutedContext.Exception as HttpException;
            if (httpEx != null)
            {
                errorCode = httpEx.GetHttpCode();
            }

            var stringContent = new StringContent(JsonConvert.SerializeObject(new HttpErrorMessage()
            {
                Error = true,
                Message = httpActionExecutedContext.Exception.ShortErrorMessage()
            }));
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            httpActionExecutedContext.Response = new HttpResponseMessage((HttpStatusCode)errorCode)
            {
                Content = stringContent
            };

            base.OnException(httpActionExecutedContext);
        }
    }
}
