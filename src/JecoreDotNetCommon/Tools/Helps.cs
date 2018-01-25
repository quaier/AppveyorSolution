using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace JecoreDotNetCommon.Tools
{
    public class Helps
    {
        /// <summary>
        /// //获取ip
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
            {
                ip = Convert.ToString(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            }
            if (string.IsNullOrEmpty(ip))
            {
                ip = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }
            return ip;
        }

        public static string GetRealIP()
        {
            string result = String.Empty;
            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //可能有代理   
            if (!string.IsNullOrWhiteSpace(result))
            {
                //没有"." 肯定是非IP格式  
                if (result.IndexOf(".") == -1)
                {
                    result = null;
                }
                else
                {
                    //有","，估计多个代理。取第一个不是内网的IP。  
                    if (result.IndexOf(",") != -1)
                    {
                        result = result.Replace(" ", string.Empty).Replace("\"", string.Empty);

                        string[] temparyip = result.Split(",;".ToCharArray());

                        if (temparyip != null && temparyip.Length > 0)
                        {
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                //找到不是内网的地址  
                                if (IsIPAddress(temparyip[i]) && temparyip[i].Substring(0, 3) != "10." && temparyip[i].Substring(0, 7) != "192.168" && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];
                                }
                            }
                        }
                    }
                    //代理即是IP格式  
                    else if (IsIPAddress(result))
                    {
                        return result;
                    }
                    //代理中的内容非IP  
                    else
                    {
                        result = null;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
        public static bool IsIPAddress(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 7 || str.Length > 15)
                return false;

            string regformat = @"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);

            return regex.IsMatch(str);
        }

        /// <summary>
        /// 路径拼接 替代 Path.Combine 
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string PathCombine(params string[] paths)
        {
            if (paths.Length == 0)
            {
                throw new ArgumentException("please input path");
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                string spliter = "\\";
                string firstPath = paths[0];
                if (firstPath.StartsWith("HTTP", StringComparison.OrdinalIgnoreCase))
                {
                    spliter = "/";
                }
                if (!firstPath.EndsWith(spliter))
                {
                    firstPath = firstPath + spliter;
                }
                builder.Append(firstPath);
                for (int i = 1; i < paths.Length; i++)
                {
                    string nextPath = paths[i];
                    if (nextPath.StartsWith("/") || nextPath.StartsWith("\\"))
                    {
                        nextPath = nextPath.Substring(1);
                    }
                    if (i != paths.Length - 1)//not the last one
                    {
                        if (nextPath.EndsWith("/") || nextPath.EndsWith("\\"))
                        {
                            nextPath = nextPath.Substring(0, nextPath.Length - 1) + spliter;
                        }
                        else
                        {
                            nextPath = nextPath + spliter;
                        }
                    }
                    builder.Append(nextPath);
                }
                return builder.ToString();
            }
        }
    }
}
