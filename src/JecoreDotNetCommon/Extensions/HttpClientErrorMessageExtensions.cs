using System;
using JecoreDotNetCommon.Models;
using Newtonsoft.Json;

namespace JecoreDotNetCommon.Extensions
{
    /// <summary>
    /// 生成错误消息模型
    /// </summary>
    public static class HttpClientErrorMessageExtensions
    {
        public static string GetErrorMessage(this HttpClientOperationResult res)
        {
            if (res.ResultType == HttpClientOperationResultType.Success)
            {
                throw new Exception("当前操作状态不匹配");
            }
            else if (res.ResultType == HttpClientOperationResultType.Error)
            {
                //取出其中的Message字段
                dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(res.AppendData.ToString());
                string message = "";
                if (jsonData != null)
                {
                    if (jsonData["Message"] != null)
                    {
                        message = jsonData.Message;
                    }
                    else
                    {
                        message = jsonData;
                    }
                }
                if (string.IsNullOrEmpty(message))
                {
                    message = res.Message;
                }
                return message;
            }
            else
            {
                throw new Exception("OperationResult 转换时错误 参数不足 ");
            }
        }
    }
}
