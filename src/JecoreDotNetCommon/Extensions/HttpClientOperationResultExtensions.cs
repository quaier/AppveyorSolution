using System;
using JecoreDotNetCommon.Models;
using JecoreDotNetCommon.Helper;

namespace JecoreDotNetCommon.Extensions
{
    public static class HttpClientOperationResultExtensions
    {
        public static RetJsonResultView ToJsonResult(this HttpClientOperationResult res)
        {
            if (res.ResultType == HttpClientOperationResultType.Success)
            {
                return new RetJsonResultView
                {
                    OperationResultType = HttpClientOperationResultType.Success.CastTo<int>(),
                    Message = res.Message ?? res.ResultType.ToDescription()
                };
            }
            else if (res.ResultType == HttpClientOperationResultType.Error)
            {
                return new RetJsonResultView
                {
                    OperationResultType = HttpClientOperationResultType.Error.CastTo<int>(),
                    Message = res.Message ?? res.ResultType.ToDescription()
                };
            }
            else
            {
                throw new Exception("OperationResult 转换时错误 参数不足 ");
            }
        }
    }
    public class RetJsonResultView
    {
        public int OperationResultType { set; get; }
        public string Message { set; get; }

    }
}
