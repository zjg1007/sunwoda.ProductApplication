using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// UpdateShipCart传入参数
    /// </summary>
    public class AgitationUpdateShipCartParam
    {
        /// <summary>
        /// 固含量
        /// </summary>
        public string solid_content { get; set; }
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipment_no { get; set; }
        /// <summary>
        /// 粘度
        /// </summary>
        public string viscosity { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string USER_NAME { get; set; }
    }
}
