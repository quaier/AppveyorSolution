using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace JecoreDotNetCommon.Web.Mvc
{
    /// <summary>
    /// Cookies操作类
    /// 使用FormsAuthentication认证
    /// </summary>
    public static class CookiesHelper
    {
        /// <summary>
        /// 获取当前cookies对应的用户信息
        /// </summary>
        /// <param name="httpContext"></param>
        public static T GetUserInfo<T>(HttpContextBase httpContext)
        {

            if (httpContext.User == null)
            {
                return default(T);
            }
            HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return default(T);
            }
            try
            {
                // 解密 
                T _member;
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                _member = JsonConvert.DeserializeObject<T>(authTicket.UserData);
                return _member;
            }
            catch (Exception)
            {
                Logout(httpContext);
                return default(T);
            }
        }

        /// <summary>
        /// 获取当前cookie对象
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static HttpCookie GetCookie(HttpContextBase httpContext)
        {
            return httpContext.Response.Cookies[FormsAuthentication.FormsCookieName];
        }

        /// <summary>
        /// 写入cookie
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="username"></param>
        /// <param name="data"></param>
        public static void AddCookie(HttpContextBase httpContext, string username, object data)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (1,
                    username,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(20),
                    true,
                    JsonConvert.SerializeObject(data),
                    "/"
                );

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket));

            cookie.HttpOnly = true;

            httpContext.Response.Cookies.Add(cookie);
        }

        public static void UpdateCookie(HttpContextBase httpContext, string username, object data)
        {

        }

        /// <summary>
        /// 注销当前Cookies
        /// </summary>
        /// <param name="httpContext"></param>
        public static void Logout(HttpContextBase httpContext)
        {
            HttpCookie cookie = httpContext.Response.Cookies[FormsAuthentication.FormsCookieName];

            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddMonths(-1);
            httpContext.Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
        }


    }
}
