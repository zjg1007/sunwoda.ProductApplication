using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// 搅拌详细物料类
    /// </summary>
  public   class AgitationItems
    {
        /// <summary>
        /// 物料名称
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 物料批次
        /// </summary>
        public string item_Batch { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        public string item_No { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string item_Number { get; set; }
        /// <summary>
        /// 投入数量
        /// </summary>
        public string putNumber { get; set; }
    }
}
