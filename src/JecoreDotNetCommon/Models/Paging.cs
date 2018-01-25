using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Models
{
    public class Paging
    {
        /// <summary>
        /// 页码数
        /// </summary>
        public int PageId { get; set; }

        /// <summary>
        /// 页面描述
        /// </summary>
        public string Pname { get; set; }

        /// <summary>
        /// 是否为选中页面
        /// </summary>
        public bool Cur { get; set; }

        public string ConditionStr { get; set; }
    }
}
