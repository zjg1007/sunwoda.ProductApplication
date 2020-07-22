using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// DeviceSelect传入参数
    /// </summary>
    public class DeviceSelectParam
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_sn { get; set; }
        /// <summary>
        /// 项目名称(组别相关的Package列表)
        /// </summary>
        public string productType { get; set; }
    }
}
