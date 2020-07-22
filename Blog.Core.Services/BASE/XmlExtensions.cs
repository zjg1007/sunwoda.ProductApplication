using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Blog.Core.Common;
using PMESWeb;
using StackExchange.Profiling.Internal;

namespace Blog.Core.Services.BASE
{
   public  static class XmlExtensions
    {
        /// <summary>
        /// 扩展方法XML转实体 2019-12-18 zjg
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<TEntity> DataToXml<TEntity>(this GetSqlDataResponseGetSqlDataResult data) where TEntity : class
        {
            List<TEntity> listModel = new List<TEntity>();
            //string str = typeof(TEntity).Name;
            foreach (XmlNode el in data.Any1.GetElementsByTagName("Table"))
            {
                string str = typeof(TEntity).Name;
                string entiry = @"<"+str+">" +el.InnerXml+ @"</" + str + ">";
                //el.Name = str;

                TEntity data1 = SerializeHelper.DESerializerStringToEntity<TEntity>(entiry);
                listModel.Add(data1);
            }
            return listModel;
        }
    }
}
