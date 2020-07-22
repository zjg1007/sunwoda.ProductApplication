using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    public class t_gule_step_infoVM
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
        public int STEP_NUMBER { get; set; }
        /// <summary>
        /// 工步名称
        /// </summary>
        public string STEP_NAME { get; set; }
        /// <summary>
        /// 加料名称
        /// </summary>
        public string  POWDER_MATERIAL { get; set; }
        /// <summary>
        /// 加料值（页面传值）
        /// </summary>
        public float POWDER_Value { get; set; }
        /// <summary>
        /// 加料上限值
        /// </summary>
        public float POWDER_UPPER { get; set; }
        /// <summary>
        /// 加料下限值
        /// </summary>
        public float POWDER_LOWER { get; set; }
        /// <summary>
        /// 搅拌数据值
        /// </summary>
        public float MIXING_Value { get; set; }
        /// <summary>
        /// 搅拌速度上限值
        /// </summary>
        public float MIXING_SPEED_UPPER { get; set; }
        /// <summary>
        /// 搅拌速度下限值
        /// </summary>
        public float MIXING_SPEED_LOWER { get; set; }
        /// <summary>
        /// 分离速度值
        /// </summary>
        public float SEPARATE_Value { get; set; }
        /// <summary>
        /// 分离速度上限值
        /// </summary>
        public float SEPARATE_UPPER { get; set; }
        /// <summary>
        /// 分离速度下限值
        /// </summary>
        public float SEPARATE_LOWER { get; set; }
        /// <summary>
        /// 搅拌时间
        /// </summary>
        public int MIXING_TIME { get; set; }
        /// <summary>
        /// 真空值值
        /// </summary>
        public float VACUUM_Value { get; set; }
        /// <summary>
        /// 真空值上限值
        /// </summary>
        public float VACUUM_UPPER { get; set; }
        /// <summary>
        /// 真空值下限值
        /// </summary>
        public float?VACUUM_LOWER { get; set; }
        /// <summary>
        /// 温度值
        /// </summary>
        public float TEMPER_Value { get; set; }
        /// <summary>
        /// 温度上限
        /// </summary>
        public float TEMPER_UPPER { get; set; }
        /// <summary>
        /// 温度下限
        /// </summary>
        public float TEMPER_LOWER { get; set; }
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
        
        /// <summary>
        /// 制令单
        /// </summary>
        public string package { get; set; }
        /// <summary>
        /// 选用的模板(true:光明 false:惠州)
        /// </summary>
        public bool template { get; set; }
    }
}
