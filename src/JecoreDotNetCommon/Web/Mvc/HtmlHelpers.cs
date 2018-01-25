using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JecoreDotNetCommon.Web.Mvc
{
    public static class HtmlHelpers
    {
        private class SectionBlock : IDisposable
        {
            WebViewPage WebPageBase;

            private static string SectionName = "";

            public static List<string> GetSectionList(string sectionName)
            {
                if (!string.IsNullOrEmpty(SectionName))
                {
                    if (HttpContext.Current.Items[SectionName] == null)
                    {
                        HttpContext.Current.Items[SectionName] = new List<string>();
                    }
                    return (List<string>)HttpContext.Current.Items[sectionName];
                }
                return new List<string>();
            }

            public SectionBlock(WebViewPage webPageBase, string sectionName)
            {
                SectionName = sectionName;
                WebPageBase = webPageBase;
                WebPageBase.OutputStack.Push(new StringWriter());
            }

            public void Dispose()
            {
                var sectionList = GetSectionList(SectionName);
                sectionList.Add(((StringWriter)WebPageBase.OutputStack.Pop()).ToString());
            }
        }

        /// <summary>
        /// 配置内容，该内容将在母版页中指定的section内呈现
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static IDisposable Section(this HtmlHelper helper, string sectionName)
        {
            return new SectionBlock((WebViewPage)helper.ViewDataContainer, sectionName);
        }

        /// <summary>
        /// 在布局页中，将呈现指定部分的内容
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderSection(this HtmlHelper helper, string sectionName)
        {
            var sectionList = SectionBlock.GetSectionList(sectionName);
            if (sectionList != null && sectionList.Count > 0)
            {
                return MvcHtmlString.Create(string.Join(Environment.NewLine, sectionList.Select(s => s.ToString())));
            }
            return MvcHtmlString.Create(string.Empty);
        }

        /// <summary>
        /// 判断当前的路由是否符合给定的路由表
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public static string IndexOfCurrentRouter(string[] routes, string selected)
        {
            return selected;
        }
    }
}