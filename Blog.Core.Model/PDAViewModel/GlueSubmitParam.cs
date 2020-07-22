using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// GlueSubmit传入参数
    /// </summary>
    public class GlueSubmitParam
    {
        /// <summary>
        /// 项目名称 例：负极打胶
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_sn { get; set; }
        /// <summary>
        /// 获取出货牌时间（获取的出货牌信息，GetShipCard接口带出来的信息）
        /// </summary>
        public string sys_Date { get; set; }
        /// <summary>
        /// 出货牌（获取的出货牌信息，GetShipCard接口带出来的信息）
        /// </summary>
        public string shipCard { get; set; }
        /// <summary>
        /// 溶液总量
        /// </summary>
        public string solution_SUM { get; set; }
        /// <summary>
        /// 出货牌计算变量（获取的出货牌信息，GetShipCard接口带出来的信息）
        /// </summary>
        public string strLSH { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string glueUser { get; set; }
        /// <summary>
        /// 制令单
        /// </summary>
        public string moNumber { get; set; }
        /// <summary>
        /// 溶液名称
        /// </summary>
        public string solutions_Name { get; set; }
        /// <summary>
        /// 溶液名称
        /// </summary>
        public string cmbSolutions_name { get; set; }
        /// <summary>
        /// 出货数量-默认0
        /// </summary>
        public int outQTY { get; set; }

    }
}
