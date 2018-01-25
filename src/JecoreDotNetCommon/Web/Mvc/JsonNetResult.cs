using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace JecoreDotNetCommon.Web.Mvc
{
    /// <summary>
    /// 覆盖默认  JsonResult 中JavascriptSerializer 序列化json 改用 json.net 序列化对象。处理日期问题
    /// </summary>
    public class JsonNetResult : JsonResult
    {
        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };
        }

        public JsonSerializerSettings Settings { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new InvalidOperationException("JSON GET is not allowed");
            //}

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            // 修复IE11以下的版本中 将JSON响应作为可下载文件的BUG
            if (context.HttpContext.Request.Browser.Type.Contains("InternetExplorer"))
            {
                double version = 0;
                double.TryParse(context.HttpContext.Request.Browser.Version, out version);
                if (version <= 11.0)
                {
                    response.ContentType = "text/html";
                }
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data == null)
            {
                return;
            }

            var scriptSerializer = JsonSerializer.Create(Settings);

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, Data);
                response.Write(sw.ToString());
            }
        }
    }
}
