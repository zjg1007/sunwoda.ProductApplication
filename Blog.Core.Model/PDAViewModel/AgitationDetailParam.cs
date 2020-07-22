using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    /// <summary>
    /// AgitationDetail传入参数
    /// </summary>
    public class AgitationDetailParam
    {
        /// <summary>
        /// 出货牌
        /// </summary>
        public string shipCard { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_no { get; set; }
        /// <summary>
        /// 当前步骤
        /// </summary>
        public string stepName { get; set; }
        /// <summary>
        /// 工步号
        /// </summary>
        public  int STEP_NUMBER { get; set; }
        /// <summary>
        /// 下一步骤
        /// </summary>
        public string next_step { get; set; }
        /// <summary>
        /// 上一步骤
        /// </summary>
        public string last_step { get; set; }
        /// <summary>
        ///物料信息
        /// </summary>
        public List<AgitationItems> itemList { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// IPQC
        /// </summary>
        public string MONITOR { get; set; }
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
        public AgitationDetailParam()
        {
            itemList = new List<AgitationItems>();
        }

        public AgitationStrstepNameSelecteParam stepModel { get; set; }
        public t_gule_step_infoVM guleStepModel { get; set; }
    }
}
