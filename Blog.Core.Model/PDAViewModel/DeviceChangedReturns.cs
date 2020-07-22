using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    public class DeviceChangedReturns
    {
        /// <summary>
        /// 当前工序出货牌
        /// </summary>
        public string shipment_no { get; set; }
        /// <summary>
        /// 上一节点出货牌
        /// </summary>
        public string previous_shipment_no { get; set; }
        /// <summary>
        /// PACKAGE号/生产单号
        /// </summary>
        public string technology { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 投入量
        /// </summary>
        public string input_sum { get; set; }
        /// <summary>
        /// 工程师
        /// </summary>
        public string engineer { get; set; }
        /// <summary>
        /// IPQC
        /// </summary>
        public string monitor { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 出货数量/数量
        /// </summary>
        public string out_qty { get; set; }
        /// <summary>
        /// 剩余数量
        /// </summary>
        public string qty { get; set; }
        /// <summary>
        /// 卷绕批号（辊压工序传入）
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 分条数（分条工序传入）
        /// </summary>
        public string Partition_Sum { get; set; }
        /// <summary>
        /// X&Y （分条工序传入）
        /// </summary>
        public string Partition_XY { get; set; }
    }
}
