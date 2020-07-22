using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// GetShipCard传入参数
    /// </summary>
    public class GetShipCardReturns
    {
        /// <summary>
        /// 出货牌，注：临时生成的出货牌，还没有录入到数据库
        /// </summary>
        public string shipCard { get; set; }
        /// <summary>
        /// 获取出货牌时间
        /// </summary>
       // public string sys_Date { get; set; }
        /// <summary>
        /// 出货牌计算变量
        /// </summary>
        //public string strLSH { get; set; }
    }
}
