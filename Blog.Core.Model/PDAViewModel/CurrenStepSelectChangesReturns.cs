using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    public class CurrenStepSelectChangesReturns
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
        public string PublicSpeed { get; set; }
        /// <summary>
        /// 自转转速
        /// </summary>
        public string SelfVelocity { get; set; }
        /// <summary>
        /// 搅拌时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public string Temperature { get; set; }
        /// <summary>
        /// 真空度
        /// </summary>
        public string Vacuum { get; set; }
        /// <summary>
        /// 粘度
        /// </summary>
        public string Viscosity { get; set; }
        /// <summary>
        /// 细度
        /// </summary>
        public string Fineness { get; set; }
        /// <summary>
        /// 过筛况
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 除铁要求
        /// </summary>
        public string chutie { get; set; }
        /// <summary>
        /// ItemName1
        /// </summary>
        public string ItemName1 { get; set; }
        /// <summary>
        /// ItemValue1
        /// </summary>
        public string ItemValue1 { get; set; }
        /// <summary>
        /// Weight1
        /// </summary>
        public string Weight1 { get; set; }
        public string ItemName2 { get; set; }
        public string ItemValue2 { get; set; }
        public string Weight2 { get; set; }
        public string ItemName3 { get; set; }
        public string ItemValue3 { get; set; }
        public string Weight3 { get; set; }
        public string ItemName4 { get; set; }
        public string ItemValue4 { get; set; }
        public string Weight4 { get; set; }
    }
}
