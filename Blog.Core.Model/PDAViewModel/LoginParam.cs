using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAViewModel
{
    public  class LoginParam
    {
        /// <summary>
        /// 制令单
        /// </summary>
        public string packageNo { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string deviceNo { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string groupName { get; set; }
    }
}
