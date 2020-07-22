using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// GlueClose传入参数
    /// </summary>
    public class GlueCloseParam
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public  string productType { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_sn { get; set; }
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipCard { get; set; }
        /// <summary>
        /// 下个工序名称
        /// </summary>
        public string productItem { get; set; }
        /// <summary>
        /// 出货数量
        /// </summary>
        public string outQTY { get; set; }
        /// <summary>
        /// 制令单
        /// </summary>
        public string moNumber { get; set; }
    }
}
