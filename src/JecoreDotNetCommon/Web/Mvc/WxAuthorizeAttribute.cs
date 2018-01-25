using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace JecoreDotNetCommon.Web.Mvc
{
    public class WxAuthorizeAttribute : ActionFilterAttribute, IActionFilter
    {

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //从cookie 获取微信用户绑定信息。如果为空 判断用户没有绑定过微信账号。流程跳转到绑定页面
            var openid = filterContext.HttpContext.Request.Cookies.Get("wxOpenid");
            var acessToken = filterContext.HttpContext.Request.Cookies.Get("acessToken");

            if (openid == null || openid.ToString() == "" 
                || acessToken == null || acessToken.ToString()=="")
            {
                filterContext.Result = new RedirectToRouteResult("WeiXin_default",
                new RouteValueDictionary(new
                {
                    area = "WeiXin",
                    action = "Index",
                    controller = "OAuth2"
                })); 
            }
        }
    }
}
