using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// Close传入参数
    /// </summary>
    public class CloseParam
    {
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipment_no { get; set; }
        /// <summary>
        /// 出货数量
        /// </summary>
        public string out_qty { get; set; }
        /// <summary>
        /// 投入数量
        /// </summary>
        public string input_Sum { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 下一步骤
        /// </summary>
        //public string productItem { get; set; }
        /// <summary>
        /// 制令单
        /// </summary>
        public string moNumber { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_sn { get; set; }
    }
}
