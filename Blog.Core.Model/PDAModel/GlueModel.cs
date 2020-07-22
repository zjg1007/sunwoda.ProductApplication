using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAModel
{
    /// <summary>
    /// 打胶工序
    /// </summary>
    public class GlueModel
    {

        //页面加载获取设备信息的实体
        public string device_sn { get; set; }
        public string device_id { get; set; }
        public string work_stationname { get; set; }

        //获取出货牌的实体
        /// <summary>
        /// 项目类型
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string productCode { get; set; }
        public string s_number { get; set; }
        public string s_date { get; set; }
        public  string sys_Date { get; set; }
        /// <summary>
        /// 返回值：出货批号
        /// </summary>
        public string shipCard { get; set; }

        //提交打胶需要提供的信息
        /// <summary>
        /// 溶液总量
        /// </summary>
        public string solution_SUM { get; set; }
        /// <summary>
        /// 出货牌计算变量
        /// </summary>
        public string strLSH { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string glueUser { get; set; }
        /// <summary>
        /// 制令单
        /// </summary>
        public string moNumber { get; set; }
        /// <summary>
        /// 溶液名称
        /// </summary>
        public string solutions_Name { get; set; }
        public string cmbSolutions_name { get; set; }
        /// <summary>
        /// 出货数量
        /// </summary>
        public int outQTY { get; set; }
        /// <summary>
        /// 下个工序名称
        /// </summary>
        public string productItem { get; set; }
        /// <summary>
        /// 投入数量
        /// </summary>
        public int input_sum { get; set; }

        public string USER_NAME { get; set; }
        public  string item_value { get; set; }

    }
}
