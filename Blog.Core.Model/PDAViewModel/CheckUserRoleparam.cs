using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    public class CheckUserRoleparam
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 上一节点出货牌
        /// </summary>
        public string previous_shipment_no { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_no { get; set; }
        /// <summary>
        /// PACKAGE号/生产单号
        /// </summary>
        public string technology { get; set; }
    }
}
