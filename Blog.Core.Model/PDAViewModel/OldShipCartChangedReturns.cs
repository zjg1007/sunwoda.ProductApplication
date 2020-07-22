using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
   public  class OldShipCartChangedReturns
    {
        /// <summary>
        /// 工程师
        /// </summary>
        public string engineer { get; set; }
        /// <summary>
        /// PACKAGE号/生产单号
        /// </summary>
        public string technology { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        //public string type_name { get; set; }
        /// <summary>
        /// 出货数量
        /// </summary>
        public string out_qty { get; set; }
        /// <summary>
        /// 剩余量
        /// </summary>
        public string sY_QTY { get; set; }
    }
}
