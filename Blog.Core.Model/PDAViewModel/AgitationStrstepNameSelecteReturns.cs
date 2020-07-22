using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// 搅拌详细传入参数
    /// </summary>
    public  class AgitationStrstepNameSelecteReturns
    {
        /// <summary>
        /// IPQC
        /// </summary>
        public string MONITOR { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 公转转速
        /// </summary>
        public string publicSpeed { get; set; }
        /// <summary>
        /// 自转转速
        /// </summary>
        public string selfVelocity { get; set; }
        /// <summary>
        /// 搅拌时间
        /// </summary>
        public string stirTime { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public string temperature { get; set; }
        /// <summary>
        /// 真空度
        /// </summary>
        public string vacuum { get; set; }
        /// <summary>
        /// 粘度
        /// </summary>
        public string viscosity { get; set; }
        /// <summary>
        /// 细度
        /// </summary>
        public string fineness { get; set; }
        /// <summary>
        /// 过筛况
        /// </summary>
        public string filter { get; set; }
        /// <summary>
        /// 除铁要求
        /// </summary>
        public string chutie { get; set; }
        /// <summary>
        /// 下一个步骤
        /// </summary>
        public string next_step { get; set; }
        /// <summary>
        /// 物料信息
        /// </summary>
        public List<AgitationItems> items { get; set; }
        public AgitationStrstepNameSelecteReturns()
        {
            this.items = new List<AgitationItems>();
        }
    }
}
