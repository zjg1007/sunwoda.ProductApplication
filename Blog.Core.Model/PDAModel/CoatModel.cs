using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAModel
{
    /// <summary>
    /// 涂布
    /// </summary>
  public   class CoatModel
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_no { get; set; }
        /// <summary>
        /// 当前工序出货牌
        /// </summary>
        public string shipment_no { get; set; }
        /// <summary>
        /// 上一节点出货牌
        /// </summary>
        public string previous_shipment_no { get; set; }
        /// <summary>
        /// 更新出货牌
        /// </summary>
        public string shipCard_update { get; set; }
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
        /// 投入总数
        /// </summary>
        public string qty { get; set; }
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
        /// 出货数量
        /// </summary>
        public string out_qty { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string type_name { get; set; }
        /// <summary>
        /// 涂布日期
        /// </summary>
        public string coatingdate { get; set; }
        /// <summary>
        /// 时差
        /// </summary>
        public string timeDifference { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 旧流水号
        /// </summary>
        public string s_number { get; set; }
        /// <summary>
        /// 新流水号
        /// </summary>
        public string strLSH { get; set; }
        public string s_date { get; set; }
        public string sys_Date { get; set; }
        /// <summary>
        /// 拼接出货牌时间
        /// </summary>
        public string strsysDate { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        public string StrMaterialNO { get; set; }
        /// <summary>
        /// 上个出货牌的制令单
        /// </summary>
        public string Package { get; set; }
        /// <summary>
        /// 下一个工序
        /// </summary>
        public string strItem { get; set; }
        /// <summary>
        /// 制令单ID
        /// </summary>
        public string package_id { get; set; }
        /// <summary>
        /// 参数名
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string itemValue { get; set; }
        /// <summary>
        /// BOM物料ID
        /// </summary>
        public string cbd_bomid { get; set; }
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
