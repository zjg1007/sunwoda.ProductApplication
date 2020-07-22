using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// StrstepNameSelecte传入参数
    /// </summary>
    public class AgitationStrstepNameSelecteParam
    {
        /// <summary>
        /// 当前步骤名称
        /// </summary>
        public string stepName { get; set; }
        
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_value { get; set; }
    }
}
