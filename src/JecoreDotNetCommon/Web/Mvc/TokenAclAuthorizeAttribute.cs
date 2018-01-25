using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JecoreDotNetCommon.Web.Mvc
{
    public class TokenAclAuthorizeAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// Check if the user in any of the roles passed in to be checked
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //Check we have a valid HttpContext
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");


            //Check to make sure the user is authenticated
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;


            var roles = Roles.Split(',');


            //Check that we have had one or more roles passed in
            if (roles.Count() == 0)
                throw new ArgumentNullException("roles");

            
            foreach (var role in roles)
            {
                if (HttpContext.Current.User.IsInRole(role))
                //if (httpContext.User.IsInRole(role))
                {
                    return true;
                }
            }

            httpContext.Response.StatusCode = 403; 
            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                filterContext.Result = new RedirectResult("/Error/AccessDenied");
            }
        }
    } 
}
