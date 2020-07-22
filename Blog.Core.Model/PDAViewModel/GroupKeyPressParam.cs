using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// GroupKeyPress传入参数
    /// </summary>
    public class GroupKeyPressParam
    {
        /// <summary>
        /// 制令单
        /// </summary>
        public string moNumber { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string productType { get; set; }
    }
}
