using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// GroupKeyPress返回参数
    /// </summary>
    public class GroupKeyPressReturns
    {
        /// <summary>
        /// 制令单ID
        /// </summary>
        public string package_id { get; set; }
        /// <summary>
        /// 制令单编号
        /// </summary>
        public string packagenumber { get; set; }
        /// <summary>
        /// 工程师
        /// </summary>
        public string engineer { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string cell_model { get; set; }
    }
}
