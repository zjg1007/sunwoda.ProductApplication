using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.PDAModel
{
    /// <summary>
    /// 程序更新版本
    /// </summary>
   public  class APP_VersionModel
    {
        /// <summary>
        /// 程式名称
        /// </summary>
        public string APP_NAME { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FILE_NAME { get; set; }
        /// <summary>
        /// 新版本
        /// </summary>
        public string NEW_VERSION { get; set; }
        /// <summary>
        /// 旧版本
        /// </summary>
        public string OLD_VERSION { get; set; }
        /// <summary>
        /// 更新链接
        /// </summary>
        public string UPDATE_URL { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CREATE_DATE { get; set; }
        /// <summary>
        /// UPDATE_USER
        /// </summary>
        public string UPDATE_USER { get; set; }
    }
}
