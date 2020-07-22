using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// AgitationGetItemInfo返回参数
    /// </summary>
    public class AgitationGetItemInfoReturns
    {
        /// <summary>
        /// 物料名称
        /// </summary>
        public string StrMaterialName { get; set; }
        /// <summary>
        /// 物料料号
        /// </summary>
        public string StrMaterialNO { get; set; }
        /// <summary>
        /// 物料批号
        /// </summary>
        public string StrMaterialLot { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string StrMaterialNumber { get; set; }

    }
}
