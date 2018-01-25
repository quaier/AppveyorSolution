using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JecoreDotNetCommon.Extensions;

namespace JecoreDotNetCommon.Web.Mvc
{
    public class JsonExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
                // 跳过 iis 默认错误返回
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.Result = new JsonNetResult
                {
                    Data = new
                    {
                        Error = true,
                        Message = filterContext.Exception.ShortErrorMessage()
                    },
                    //Data = new
                    //{
                    //    errorMessage = filterContext.Exception.ShortErrorMessage()
                    //},
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }
    }
}