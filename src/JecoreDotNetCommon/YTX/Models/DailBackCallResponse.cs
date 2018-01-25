using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.YTX.Models
{
    /// <summary>
    ///  0	提交成功	
    /// -1	号码无效	
    /// -2	缺少必要参数 调用接口时，请传入足够的参数
    /// -3	无效action 传入正确的action
    /// -4	无效的JSON 请遵守正确json语法
    /// -100	异常错误 异常错误
    /// -101	参数不合法 传入符合要求的参数
    /// -102	电话号码不合法 传入正确的电话号码
    /// -200	无此用户 登录官网开通服务账号
    /// -201	用户状态失效 登录官网重新激活服务账号
    /// -202	包头验证信息错误 包头验证信息超时，默认有效时间十分钟，请在有效时间内完成请求
    /// -203	AuthToken验证失败 登录官网获取正确的AuthToken
    /// -204	账户余额不足 登录官网充值
    /// -205	应用验证失败 详情请看[优先必读]
    /// 未知错误
    /// </summary>
    public class DailBackCallResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusCode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string statusMsg { get; set; }

        public string requestId { get; set; }
    }
}
