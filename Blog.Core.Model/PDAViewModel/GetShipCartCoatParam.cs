using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    public class GetShipCartCoatParam
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// PACKAGE号/生产单号
        /// </summary>
        public string technology { get; set; }
    }
}
