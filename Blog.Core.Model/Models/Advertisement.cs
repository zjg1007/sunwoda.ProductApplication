using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    public class Advertisement : RootEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// 这里之所以没用RootEntity，是想保持和之前的数据库一致，主键是bID，不是Id
        //[SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        //public int ID { get; set; }
        /// <summary>
        /// 广告图片
        /// </summary>
        [SugarColumn(Length = 512, IsNullable = true)]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string Title { get; set; }

        /// <summary>
        /// 广告链接
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string Url { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createdate { get; set; } = DateTime.Now;
    }
}
