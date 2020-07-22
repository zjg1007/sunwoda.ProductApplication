using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAModel
{
    /// <summary>
    /// 搅拌工步维护表（新建）
    /// </summary>
    public class t_gule_step_info
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 加料的物料号
        /// </summary>
        public string ITEM_CODE { get; set; }
        /// <summary>
        /// 工步号
        /// </summary>
        public int  STEP_NUMBER { get; set; }
        /// <summary>
        /// 工步名称
        /// </summary>
        public string STEP_NAME { get; set; }
        /// <summary>
        /// 加料名称
        /// </summary>
        public int  POWDER_MATERIAL { get; set; }
        /// <summary>
        /// 加料上限值
        /// </summary>
        public int POWDER_UPPER { get; set; }
        /// <summary>
        /// 加料下限值
        /// </summary>
        public int POWDER_LOWER { get; set; }
        /// <summary>
        /// 搅拌速度上限值
        /// </summary>
        public int MIXING_SPEED_UPPER { get; set; }
        /// <summary>
        /// 搅拌速度下限值
        /// </summary>
        public string MIXING_SPEED_LOWER { get; set; }
        /// <summary>
        /// 分离速度上限值
        /// </summary>
        public int SEPARATE_UPPER { get; set; }
        /// <summary>
        /// 分离速度下限值
        /// </summary>
        public int SEPARATE_LOWER { get; set; }
        /// <summary>
        /// 搅拌时间
        /// </summary>
        public int MIXING_TIME { get; set; }
        /// <summary>
        /// 真空值上限值
        /// </summary>
        public int VACUUM_UPPER { get; set; }
        /// <summary>
        /// 真空值下限值
        /// </summary>
        public int VACUUM_LOWER { get; set; }
        /// <summary>
        /// 温度上限
        /// </summary>
        public int TEMPER_UPPER { get; set; }
        /// <summary>
        /// 温度下限
        /// </summary>
        public int TEMPER_LOWER { get; set; }
        /// <summary>
        /// 备注  
        /// </summary>
        public string REMARKS { get; set; }
        /// <summary>
        /// 正负极
        /// </summary>
        public string POLARITY { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UPDATE_EMP { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string MODEL_CODE { get; set; }
    }
}
