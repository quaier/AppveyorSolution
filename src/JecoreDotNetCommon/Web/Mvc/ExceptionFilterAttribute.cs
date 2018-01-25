using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JecoreDotNetCommon.Web.Mvc
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.ViewData["Error"] = filterContext.Exception;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = filterContext.Controller.ViewData,
            };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}