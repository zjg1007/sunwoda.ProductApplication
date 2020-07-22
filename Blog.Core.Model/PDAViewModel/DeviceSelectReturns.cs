using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
   public class DeviceSelectReturns
    {
        /// <summary>
        /// 组别
        /// </summary>
        public string GROUP_NAME { get; set; }
        /// <summary>
        /// Package
        /// </summary>
        public string TECHNOLOGY { get; set; }
        /// <summary>
        /// 出货批号
        /// </summary>
        public string SHIPMENT_NO { get; set; }
        public string SOLID_CONTENT { get; set; }
        /// <summary>
        /// 配方号
        /// </summary>
        public string FORMULA_NO { get; set; }
        /// <summary>
        /// 需求数量
        /// </summary>
        public string REQUIREMENT { get; set; }
        /// <summary>
        /// 投入数量
        /// </summary>
        public string INPUT_SUM { get; set; }
        /// <summary>
        /// 搅拌重量
        /// </summary>
        public string STIR_SUM { get; set; }
        /// <summary>
        /// 工程师
        /// </summary>
        public string ENGINEER { get; set; }
        /// <summary>
        /// 出货数量
        /// </summary>
        public string OUT_QTY { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>type_name
        public string TYPE_NAME { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>user_name
        public string USER_NAME { get; set; }
        /// <summary>
        /// Package_ID
        /// </summary>
        public string PACKAGE_ID { get; set; }
        /// <summary>
        /// 组别相关的Package
        /// </summary>
        public List<GroupKeyPressReturns> PACKAGE_IDS { get; set; }
    }
}
