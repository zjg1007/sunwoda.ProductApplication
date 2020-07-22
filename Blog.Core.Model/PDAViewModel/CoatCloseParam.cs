using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    public class CoatCloseParam
    {
        /// <summary>
        /// 出货数量
        /// </summary>
        public string out_qty { get; set; }
        /// <summary>
        /// 投入量
        /// </summary>
        public string input_sum { get; set; }
        /// <summary>
        /// 当前工序出货牌
        /// </summary>
        public string shipment_no { get; set; }
    }
}
