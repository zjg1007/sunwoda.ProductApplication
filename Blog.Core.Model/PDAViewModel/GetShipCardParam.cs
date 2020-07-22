using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// GetShipCard传入参数
    /// </summary>
    public class GetShipCardParam
    {
        /// <summary>
        /// 项目名称，例：正极打胶
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_sn { get; set; }
        /// <summary>
        /// 制令单
        /// </summary>
        public string package { get; set; }
        /// <summary>
        /// 配方号
        /// </summary>
        public string formula { get; set; }
    }
}
