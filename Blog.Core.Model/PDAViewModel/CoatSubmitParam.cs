using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
   public  class CoatSubmitParam
    {
        /// <summary>
        /// 是否跨Package过站
        /// </summary>
        public bool checkPermission { get; set; }
        /// <summary>
        /// 当前工序出货牌
        /// </summary>
        public string shipment_no { get; set; }
        /// <summary>
        /// 上一节点出货牌
        /// </summary>
        public string previous_shipment_no { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// PACKAGE号/生产单号
        /// </summary>
        public string technology { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 投入
        /// </summary>
        public string input_sum { get; set; }
        /// <summary>
        /// 剩余
        /// </summary>
        public string out_qty { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_no { get; set; }
        /// <summary>
        /// 工程师
        /// </summary>
        public string engineer { get; set; }
        /// <summary>
        /// IPQC
        /// </summary>
        public string monitor { get; set; }
        /// <summary>
        /// 物料信息
        /// </summary>
        public List<AgitationItems> itemList { get; set; }
        /// <summary>
        /// 物料备用参数（辊压工序传入）
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
