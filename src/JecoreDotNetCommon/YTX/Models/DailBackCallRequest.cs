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
    public class DailBackCallRequest
    {
        /// <summary>
        /// API接口名称，默认值：callDailBack（区分大小写）
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// 用户登录云通信平台后，所创建的应用编号appid，若想调用当前双向呼叫接口，则此应用必须包含有双向呼叫功能，否则调用失败。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 第一端号码（只能是一个直线固话或手机，固话前要加区号 例：153****2082）
        /// </summary>
        public string src { get; set; }

        /// <summary>
        /// 第二端号码（只能是一个直线固话或手机，固话前要加区号 例：188****2082）
        /// </summary>
        public string dst { get; set; }

        /// <summary>
        /// 通话时长限制，单位为秒，必须大于60
        /// </summary>
        public int credit { get; set; }

        /// <summary>
        /// 用户自定义参数 （主要可用为商户传给平台的唯一订单编号，本平台不校验订单唯一性,该参数在回调此订单状态时返回）
        /// </summary>
        public string customParm { get; set; }

        public string srcclid { get; set; }

        public string dstclid { get; set; }
    }
}
