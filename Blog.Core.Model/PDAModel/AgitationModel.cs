using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAModel
{
    /// <summary>
    /// 搅拌信息表
    /// </summary>
    public class AgitationModel
    {
        #region GetLookMH  参数：1  deviceNo设备编号
        /// <summary>
        /// 制令单
        /// </summary>
        public string package_id { get; set; }
        public string technology { get; set; }
        public string engineer { get; set; }
        public string group_name { get; set; }
        public string shipment_no { get; set; }
        public string formula_no { get; set; }
        public string solid_content { get; set; }
        public string requirement { get; set; }
        public string input_sum { get; set; }
        public string stir_sum { get; set; }
        public string out_qty { get; set; }
        public string USER_NAME { get; set; }
        public string TYPE_NAME { get; set; }
        #endregion
        #region  传入参数--GetLookMH（）
        /// <summary>
        /// 设备编号-设备文本改变监听事件
        /// </summary>
        public  string device_sn { get; set; }
        public string device_id { get; set; }
        public string work_stationname { get; set; }
        /// <summary>
        /// 制令单--组别回车事件
        /// </summary>
        public string moNumber { get; set; }
        /// <summary>
        /// 组别--组别回车事件
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 项目名称--组别回车事件
        /// </summary>
        public string productType { get; set; }
        public string productCode { get; set; }

        public string strLSH { get; set; }
        /// <summary>
        /// 下一步骤
        /// </summary>
        public string productItem { get; set; }
       // public string work_stationname { get; set; }
        #endregion
        #region GetLook  参数：1 moNumber 2 groupName 3 productType
        // public string package_id { get; set; }
        public string packagenumber { get; set; }
        public string group_no { get; set; }
        public string cell_model { get; set; }
        public string Plan_Fe_Number { get; set; }
       // public string engineer { get; set; }
        #endregion
        #region GetSerialnumber  参数1 productType
        public string s_number { get; set; }
        public string s_date { get; set; }
        public string sys_Date { get; set; }
        #endregion
        #region InsertSubmit 1 moNumber 2 groupName 3 shipment_no 4 Formula 5 requirement 6 input_sum 7 stir_sum 8 deviceNo 9 A_Type 10 USER_NAME 11 engineer 12 productType
        // public string shipCard { get; set; }
        public string Formula { get; set; }
        public string A_Type { get; set; }
        //public string Engineer { get; set; }
        //public string Package_ID { get; set; }
        //public string requirement { get; set; }
        //public string input_Sum { get; set; }
        #endregion
        #region InsertShipCartinfo 参数： 1 viscosity
        public string viscosity { get; set; }
        #endregion
    }
}
