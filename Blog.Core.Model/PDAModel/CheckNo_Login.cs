using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAModel
{
    public  class CheckNo_Login
    {
        /// <summary>
        /// 制令单号
        /// </summary>
        public string project_id { get; set; }
        public string cell_model { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 制令单
        /// </summary>
        public string packageNo { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string deviceNo { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string compareWith { get; set; }



    }
}
