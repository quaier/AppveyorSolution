using System.Web;
using System.Web.Mvc;
using JecoreDotNetCommon.Extensions;
using JecoreDotNetCommon.Models;

namespace JecoreDotNetCommon.Web.Mvc
{
    /// <summary>
    /// Mvc Controller 异常处理
    /// </summary>
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        //private readonly ILog _logger;

        public CustomHandleErrorAttribute()
        {
            //_logger = LogManager.GetLogger("MyLogger");
        }

        public override void OnException(ExceptionContext exceptionContext)
        {
            //if (exceptionContext.ExceptionHandled || !exceptionContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    return;
            //}

            //if (new HttpException(null, exceptionContext.Exception).GetHttpCode() != 500)
            //{
            //    return;
            //}

            //if (!ExceptionType.IsInstanceOfType(exceptionContext.Exception))
            //{
            //    return;
            //}

            // 错误状态 默认为500
            int errorCode = 500;
            HttpException httpEx = exceptionContext.Exception as HttpException;
            if (httpEx != null)
            {
                errorCode = httpEx.GetHttpCode();
            }

            // if the request is AJAX return JSON else view.
            if (exceptionContext.HttpContext.Request.IsAjaxRequest())
            {
                var jsonResult = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new HttpErrorMessage()
                    {
                        Error = true,
                        Message = exceptionContext.Exception.ShortErrorMessage()
                    }
                };

                jsonResult.ExecuteResult(exceptionContext);
                exceptionContext.ExceptionHandled = true;
            }
            else
            {
                base.OnException(exceptionContext);

                //var controllerName = (string)exceptionContext.RouteData.Values["controller"];
                //var actionName = (string)exceptionContext.RouteData.Values["action"];
                //var model = new HandleErrorInfo(exceptionContext.Exception, controllerName, actionName);

                //exceptionContext.Result = new ViewResult
                //{
                //    ViewName = View,
                //    MasterName = Master,
                //    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                //    TempData = exceptionContext.Controller.TempData
                //};
            }

            // log the error using log4net.
            //_logger.Error(exceptionContext.Exception.Message, exceptionContext.Exception);

            //exceptionContext.ExceptionHandled = true;
            //exceptionContext.HttpContext.Response.Clear();

            exceptionContext.HttpContext.Response.StatusCode = errorCode;

            exceptionContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}