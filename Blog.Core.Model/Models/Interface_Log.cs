using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using SqlSugar;

namespace Blog.Core.Model.Models
{
    [Serializable]
    [XmlRoot("Table")]
    public class Interface_Log
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public string LOGID { get; set; }
        [SugarColumn(Length = 900, IsNullable = false)]
        public string TESTRESULT { get; set; }
        [SugarColumn(Length = 900, IsNullable = false)]
        public string METHOD { get; set; }
        [SugarColumn(Length = 900, IsNullable = false)]
        public string DATETIME { get; set; }
        [SugarColumn(Length = 900, IsNullable = false)]
        public  string IPADDRESS { get; set; }
        [SugarColumn(Length = 900, IsNullable = false)]
        public string PARAMETER { get; set; }
    }
}
