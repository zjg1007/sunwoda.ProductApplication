using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAModel
{
    public  class GlueDetailModel
    {
        //页面加载需要的参数
        /// <summary>
        /// 当前步骤
        /// </summary>
        public string current_steps { get; set; }
        /// <summary>
        /// 下一个步骤
        /// </summary>
        public string next_step { get; set; }
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipCard { get; set; }

        //单前步骤改变值事件
        /// <summary>
        /// 工步名称
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 工步值
        /// </summary>
        public string item_value { get; set; }
        /// <summary>
        /// IPQC
        /// </summary>
        public string MONITOR { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 工步宽度
        /// </summary>
        public string ITEM_WEIGHT { get; set; }
        public string stepName { get; set; }
        //public string monitor { get; set; }
        //public string user { get; set; }
        /// <summary>
        /// 搅拌时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 公转转速
        /// </summary>
        public string PublicSpeed { get; set; }
        /// <summary>
        /// 自转转速
        /// </summary>
        public string SelfVelocity { get; set; }
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
        /// 出铁要求
        /// </summary>
        public string chutie { get; set; }

        public string ItemName1 { get; set; }
        public string ItemValue1 { get; set; }
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

        /// <summary>
        /// 设备名称
        /// </summary>
        public string MachineNo { get; set; }
        /// <summary>
        /// 工步数
        /// </summary>
        public string step_num { get; set; }
    }
}
