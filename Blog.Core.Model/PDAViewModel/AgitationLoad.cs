using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// Load传入参数
    /// </summary>
    public class AgitationLoad
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_sn { get; set; }
        /// <summary>
        /// 项目名称（工序和设备匹配）
        /// </summary>
        public string work_stationname { get; set; }
    }
}
