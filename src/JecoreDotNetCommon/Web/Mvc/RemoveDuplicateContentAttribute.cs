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
    /// 优化SEO，确保一个地址对应一个内容
    /// 如果多个地址对应一个内容，搜索引擎不知道你的这些地址哪个是最新的，哪个是旧的，从而可能会降低网站的权重。
    /// </summary>
    public class RemoveDuplicateContentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routes = RouteTable.Routes;
            var requestContext = filterContext.RequestContext;
            var routeData = requestContext.RouteData;
            var dataTokens = routeData.DataTokens;
            if (dataTokens["area"] == null)
                dataTokens.Add("area", "");
            var vpd = routes.GetVirtualPathForArea(requestContext, routeData.Values);
            if (vpd != null)
            {
                var virtualPath = vpd.VirtualPath.ToLower();
                var request = requestContext.HttpContext.Request;
                if (!string.Equals(virtualPath, request.Path))
                {
                    filterContext.Result = new RedirectResult(virtualPath + request.Url.Query, true);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
