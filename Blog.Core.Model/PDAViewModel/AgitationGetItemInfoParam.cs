using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// GetItemInfo传入参数
    /// </summary>
    public class AgitationGetItemInfoParam
    {
        /// <summary>
        /// 物料信息,格式：2460000291#20190101#箔材
        /// </summary>
        public string StrMaterialnfor { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string StrMaterialNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public t_gule_step_infoVM stepModel { get; set; }
    }
}
