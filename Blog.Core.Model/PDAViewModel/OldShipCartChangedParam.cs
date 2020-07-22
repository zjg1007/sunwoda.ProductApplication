using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
   public  class OldShipCartChangedParam
    {
        /// <summary>
        /// 上一节点出货牌
        /// </summary>
        public string previous_shipment_no { get; set; }
        /// <summary>
        /// 是否跨Package投入标志
        /// </summary>
        public bool checkPermission { get; set; }
        /// <summary>
        /// PACKAGE号/生产单号
        /// </summary>
        public string technology { get; set; }
    }
}
