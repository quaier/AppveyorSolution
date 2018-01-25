using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace JecoreDotNetCommon.Web.Mvc
{
    /// <summary>
    /// 移动端视图 过滤
    /// 重定向到指定的url
    /// </summary>
    public class MobileFilterAttribute : ActionFilterAttribute
    {
        public string RedirectUrl { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isMobile = filterContext.RequestContext.HttpContext.Request.Browser.IsMobileDevice;

            if (isMobile)
            {
                if (!string.IsNullOrEmpty(RedirectUrl))
                {
                    filterContext.Result = new RedirectResult(RedirectUrl);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
