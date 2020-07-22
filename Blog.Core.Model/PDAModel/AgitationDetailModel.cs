using System;
using System.Collections.Generic;
using System.Text;
using Blog.Core.Model.PDAViewModel;

namespace Blog.Core.Model.PDAModel
{
    public class AgitationDetailModel
    {
        //返回的实体集
        public string item_name { get; set; }
        public string item_value { get; set; }
        //public string monitor { get; set; }
        public string userName { get; set; }
        public string item_weight { get; set; }
        public string current_steps { get; set; }
        public string next_step { get; set; }
        public string out_qty { get; set; }
        public string TECHNOLOGY { get; set; }
        public string type_name { get; set; }
        public int CMC { get; set; }
        public int PVDF { get; set; }
        public string coatingdate { get; set; }
        /// <summary>
        /// 时差
        /// </summary>
        public DateTime timeDifference { get; set; }
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
        public string vacuum{ get; set; }
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
        public string filter{ get; set; }
        /// <summary>
        /// 除铁要求
        /// </summary>
        public string chutie { get; set; }
       public int step_num { get; set; }
        //接受的参数
        public string stepName { get; set; }
        public string shipCard { get; set; }
        public string USERNAME { get; set; }
        public string MONITOR { get; set; }
        public string ITEM_WEIGHT { get; set; }
        public string device_no { get; set; }
        public string item_number { get; set; }
        /// <summary>
        /// 工序名臣
        /// </summary>
        public string groupName { get; set; }
        public List<AgitationItems> itemList { get; set; }
        /// <summary>
        /// 物料信息--#号隔开
        /// </summary>
        public string itemValue1 { get; set; }
        //额外字段
        /// <summary>
        /// 物料编号
        /// </summary>
        public string StrMaterialNO { get; set; }
        /// <summary>
        /// 物料批次号
        /// </summary>
        public string item_Batch { get; set; }
        /// <summary>
        /// 物料物料号
        /// </summary>
        public string item_No { get; set; }
        public AgitationDetailModel() {
            this.itemList = new List<AgitationItems>();
        }
    }
}
