using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using PMESWeb;

namespace Blog.Core.Common
{
    public static class XmlExtensions
    {
        /// <summary>
        /// 扩展方法XML转实体 2019-12-18 zjg
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<TEntity> DataToXml<TEntity>(this GetSqlDataResponseGetSqlDataResult data) where TEntity : class
        {
            List < TEntity > listModel = new List<TEntity>();
            foreach (XElement el in data.Any.Nodes.Descendants("Table"))
            {
                TEntity data1 = SerializeHelper.DESerializerStringToEntity<TEntity> (el.ToString());
                listModel.Add(data1);
            }
            return listModel;
        }
     
    }
}
