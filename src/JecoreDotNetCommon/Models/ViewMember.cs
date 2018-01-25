using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Models
{
    /// <summary>
    /// 用户显示模型。 form 登录时 存储此模型在cookie中
    ///
    /// </summary>
    public class ViewMember
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserId { set; get; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 角色
        /// </summary>
        public string RoteName { set; get; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePh { set; get; }
        /// <summary>
        /// 头像
        /// </summary>
        public string User_FaceUrl { set; get; }

        public string UserToken { set; get; }
    }
}
