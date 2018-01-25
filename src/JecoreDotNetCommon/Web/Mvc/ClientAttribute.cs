using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JecoreDotNetCommon.Web.Mvc
{
    /// <summary>
    /// 客户端过滤
    /// </summary>
    public class ClientAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 允许移动端访问，默认为true
        /// </summary>
        public bool AllowedMobile = true;

        /// <summary>
        /// 允许PC端访问，默认为true
        /// </summary>
        public bool AllowedPc = true;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isMobile = filterContext.RequestContext.HttpContext.Request.Browser.IsMobileDevice;

            if ((!isMobile && !AllowedPc) || (isMobile && !AllowedMobile))
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}
