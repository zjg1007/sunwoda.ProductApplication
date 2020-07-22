using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// DeviceSelectChanges传入参数
    /// </summary>
    public class DeviceSelectChangesReturns
    {
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipCard { get; set; }
        /// <summary>
        /// 溶液总量
        /// </summary>
        public int input_sum { get; set; }
        /// <summary>
        /// 出货数量
        /// </summary>
        public int outQTY { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string USER_NAME { get; set; }
        /// <summary>
        /// 溶液名称（可能有两个溶液名称）
        /// </summary>
        public string item_value { get; set; }
    }
}
