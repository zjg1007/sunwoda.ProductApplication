using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// CurrenStepSelectChanges传入参数
    /// </summary>
    public class CurrenStepSelectChangesParam
    {
        /// <summary>
        /// 当前步骤
        /// </summary>
        public string stepName { get; set; }
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipCard { get; set; }
    }
}
