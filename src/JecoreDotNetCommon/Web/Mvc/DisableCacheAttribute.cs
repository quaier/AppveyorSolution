using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JecoreDotNetCommon.Web.Mvc
{
    public class DisableCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var cache = filterContext.HttpContext.Response.Cache;
            cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            cache.SetNoStore();
            cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            cache.SetValidUntilExpires(false);
            cache.SetCacheability(HttpCacheability.NoCache);
        }
    }
}
