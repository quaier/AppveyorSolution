using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace JecoreDotNetCommon.Web.Http
{
    public static class ModelStateExtension
    {
        /// <summary>
        /// 将 modelState 状态中的错误信息转换成字符串
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string ToErrorString(this ModelStateDictionary modelState)
        {
            var err = new List<string>();
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    err.Add(error.ErrorMessage);
                }
            }
            string p = string.Join("", err.ToArray());
            return p;
        }
    }
}
