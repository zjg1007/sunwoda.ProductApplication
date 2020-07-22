using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
   public  class CoatUpdateParam
    {
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipment_no { get; set; }
        /// <summary>
        /// 投入量(添加数量)
        /// </summary>
        public string input_sum { get; set; }
        /// <summary>
        /// 更新出货牌
        /// </summary>
        public string shipCard_update { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// IPQC
        /// </summary>
        public string monitor { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string productType { get; set; }
    }
}
