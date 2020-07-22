using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
   public  class AgitationSubmitParam
    {
        /// <summary>
        /// 制令单编号
        /// </summary>
        public string moNumber { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipment_no { get; set; }
        /// <summary>
        /// 配方号
        /// </summary>
        public string Formula { get; set; }
        /// <summary>
        /// 需求数量
        /// </summary>
        public string requirement { get; set; }
        /// <summary>
        /// 投入数量
        /// </summary>
        public string input_sum { get; set; }
        /// <summary>
        /// 搅拌重量-保留一位小数
        /// </summary>
        public string stir_sum { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_sn { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string A_Type { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string USER_NAME { get; set; }
        /// <summary>
        /// 工程师
        /// </summary>
        public string engineer { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 制令单ID
        /// </summary>
        public string packagenumber { get; set; }
    }
}
