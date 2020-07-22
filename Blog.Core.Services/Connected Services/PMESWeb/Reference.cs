﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMESWeb
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PMESWeb.MESInterfaceSoap")]
    public interface MESInterfaceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LockProduct", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> LockProductAsync(string product, string moNumber, string userCode, string errorItem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/PackLinkCellGroup", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> PackLinkCellGroupAsync(string Pack_Sn, string Cell_Sn, string M_MO, string M_MACHINENO, string M_KP_ITEMSN_ALL, string user_code);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCurShipmentNo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetCurShipmentNoAsync(string ShipmentInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StartProduction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> StartProductionAsync(string ProductionInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CheckItem", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> CheckItemAsync(string ItemInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CellBOMItemsCheck", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> CellBOMItemsCheckAsync(string M_SN, string M_MO, string M_USERNO, string M_MACHINENO, string M_KP_ITEMSN_ALL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BOMItemsCheck", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> BOMItemsCheckAsync(string M_SN, string M_MO, string M_USERNO, string M_MACHINENO, string M_KP_ITEMSN_ALL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CheckUserRole", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> CheckUserRoleAsync(string M_USERNO, string M_PASSWORD, string M_RoleName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GroupNameCheck", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GroupNameCheckAsync(string MainSN, string GroupName, string MO, string Process, string Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DeviecStatusInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> DeviecStatusInfoAsync(string deviecCode, string deviecStatus, string StartTime, string EndTime, string deviecErrorInfo, string userCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CheckUserDo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> CheckUserDoAsync(string M_USERNO, string M_PASSWORD, string M_MACHINENO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveXRAYData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> SaveXRAYDataAsync(string product_sn, string emp_no, string test_data, string test_result, string pic_path, string machine_no);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GroupTest", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GroupTestAsync(string M_SN, string M_MACHINCENO, string M_EMP);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CellGroupLink", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> CellGroupLinkAsync(string cell_list);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/TestDataGroupInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> TestDataGroupInfoAsync(string GroupInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WIPTEST", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> WIPTESTAsync(string M_SN, string M_RESULT, string M_USERNO, string M_MACHINENO, string M_ERROR, string M_ITEMVALUE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CheckProductMoNumber", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> CheckProductMoNumberAsync(string product, string moNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ExecuteNonQuery", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> ExecuteNonQueryAsync(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CellCoverInput", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> CellCoverInputAsync(string M_SN, string M_MO, string M_USERNO, string M_MACHINENO, string M_KP_ITEMSN_ALL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetSqlData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<PMESWeb.GetSqlDataResponseGetSqlDataResult> GetSqlDataAsync(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HWInterface", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> HWInterfaceAsync(string M_MACHINENO, string M_PRODUCT_SN, string M_MO, string M_EMP, string M_KP_ITEMSN_ALL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetWeightBeforeInjectionA", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetWeightBeforeInjectionAAsync(string ProductSn);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ExecuteNonQueryConn", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> ExecuteNonQueryConnAsync(string sql, string Conn);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/checkNetStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> checkNetStatusAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/OfflineDataUpload", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> OfflineDataUploadAsync(string M_SN, string M_RESULT, string M_USERNO, string M_MACHINENO, string M_ERROR, string M_ITEMVALUE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddGongBoard", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> AddGongBoardAsync(string GroupInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddDrill", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> AddDrillAsync(string GroupInfo);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/")]
    public partial class GetSqlDataResponseGetSqlDataResult
    {
        
        private System.Xml.XmlElement[] anyField;
        
        private System.Xml.XmlElement any1Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute(Namespace="http://www.w3.org/2001/XMLSchema", Order=0)]
        public System.Xml.XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute(Namespace="urn:schemas-microsoft-com:xml-diffgram-v1", Order=1)]
        public System.Xml.XmlElement Any1
        {
            get
            {
                return this.any1Field;
            }
            set
            {
                this.any1Field = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface MESInterfaceSoapChannel : PMESWeb.MESInterfaceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class MESInterfaceSoapClient : System.ServiceModel.ClientBase<PMESWeb.MESInterfaceSoap>, PMESWeb.MESInterfaceSoap
    {
        
        /// <summary>
        /// 实现此分部方法，配置服务终结点。
        /// </summary>
        /// <param name="serviceEndpoint">要配置的终结点</param>
        /// <param name="clientCredentials">客户端凭据</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public MESInterfaceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(MESInterfaceSoapClient.GetBindingForEndpoint(endpointConfiguration), MESInterfaceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MESInterfaceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(MESInterfaceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MESInterfaceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(MESInterfaceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MESInterfaceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> LockProductAsync(string product, string moNumber, string userCode, string errorItem)
        {
            return base.Channel.LockProductAsync(product, moNumber, userCode, errorItem);
        }
        
        public System.Threading.Tasks.Task<string> PackLinkCellGroupAsync(string Pack_Sn, string Cell_Sn, string M_MO, string M_MACHINENO, string M_KP_ITEMSN_ALL, string user_code)
        {
            return base.Channel.PackLinkCellGroupAsync(Pack_Sn, Cell_Sn, M_MO, M_MACHINENO, M_KP_ITEMSN_ALL, user_code);
        }
        
        public System.Threading.Tasks.Task<string> GetCurShipmentNoAsync(string ShipmentInfo)
        {
            return base.Channel.GetCurShipmentNoAsync(ShipmentInfo);
        }
        
        public System.Threading.Tasks.Task<string> StartProductionAsync(string ProductionInfo)
        {
            return base.Channel.StartProductionAsync(ProductionInfo);
        }
        
        public System.Threading.Tasks.Task<string> CheckItemAsync(string ItemInfo)
        {
            return base.Channel.CheckItemAsync(ItemInfo);
        }
        
        public System.Threading.Tasks.Task<string> CellBOMItemsCheckAsync(string M_SN, string M_MO, string M_USERNO, string M_MACHINENO, string M_KP_ITEMSN_ALL)
        {
            return base.Channel.CellBOMItemsCheckAsync(M_SN, M_MO, M_USERNO, M_MACHINENO, M_KP_ITEMSN_ALL);
        }
        
        public System.Threading.Tasks.Task<string> BOMItemsCheckAsync(string M_SN, string M_MO, string M_USERNO, string M_MACHINENO, string M_KP_ITEMSN_ALL)
        {
            return base.Channel.BOMItemsCheckAsync(M_SN, M_MO, M_USERNO, M_MACHINENO, M_KP_ITEMSN_ALL);
        }
        
        public System.Threading.Tasks.Task<string> CheckUserRoleAsync(string M_USERNO, string M_PASSWORD, string M_RoleName)
        {
            return base.Channel.CheckUserRoleAsync(M_USERNO, M_PASSWORD, M_RoleName);
        }
        
        public System.Threading.Tasks.Task<string> GroupNameCheckAsync(string MainSN, string GroupName, string MO, string Process, string Userid)
        {
            return base.Channel.GroupNameCheckAsync(MainSN, GroupName, MO, Process, Userid);
        }
        
        public System.Threading.Tasks.Task<string> DeviecStatusInfoAsync(string deviecCode, string deviecStatus, string StartTime, string EndTime, string deviecErrorInfo, string userCode)
        {
            return base.Channel.DeviecStatusInfoAsync(deviecCode, deviecStatus, StartTime, EndTime, deviecErrorInfo, userCode);
        }
        
        public System.Threading.Tasks.Task<string> CheckUserDoAsync(string M_USERNO, string M_PASSWORD, string M_MACHINENO)
        {
            return base.Channel.CheckUserDoAsync(M_USERNO, M_PASSWORD, M_MACHINENO);
        }
        
        public System.Threading.Tasks.Task<string> SaveXRAYDataAsync(string product_sn, string emp_no, string test_data, string test_result, string pic_path, string machine_no)
        {
            return base.Channel.SaveXRAYDataAsync(product_sn, emp_no, test_data, test_result, pic_path, machine_no);
        }
        
        public System.Threading.Tasks.Task<string> GroupTestAsync(string M_SN, string M_MACHINCENO, string M_EMP)
        {
            return base.Channel.GroupTestAsync(M_SN, M_MACHINCENO, M_EMP);
        }
        
        public System.Threading.Tasks.Task<string> CellGroupLinkAsync(string cell_list)
        {
            return base.Channel.CellGroupLinkAsync(cell_list);
        }
        
        public System.Threading.Tasks.Task<string> TestDataGroupInfoAsync(string GroupInfo)
        {
            return base.Channel.TestDataGroupInfoAsync(GroupInfo);
        }
        
        public System.Threading.Tasks.Task<string> WIPTESTAsync(string M_SN, string M_RESULT, string M_USERNO, string M_MACHINENO, string M_ERROR, string M_ITEMVALUE)
        {
            return base.Channel.WIPTESTAsync(M_SN, M_RESULT, M_USERNO, M_MACHINENO, M_ERROR, M_ITEMVALUE);
        }
        
        public System.Threading.Tasks.Task<string> CheckProductMoNumberAsync(string product, string moNumber)
        {
            return base.Channel.CheckProductMoNumberAsync(product, moNumber);
        }
        
        public System.Threading.Tasks.Task<string> ExecuteNonQueryAsync(string sql)
        {
            return base.Channel.ExecuteNonQueryAsync(sql);
        }
        
        public System.Threading.Tasks.Task<string> CellCoverInputAsync(string M_SN, string M_MO, string M_USERNO, string M_MACHINENO, string M_KP_ITEMSN_ALL)
        {
            return base.Channel.CellCoverInputAsync(M_SN, M_MO, M_USERNO, M_MACHINENO, M_KP_ITEMSN_ALL);
        }
        
        public System.Threading.Tasks.Task<PMESWeb.GetSqlDataResponseGetSqlDataResult> GetSqlDataAsync(string sql)
        {
            return base.Channel.GetSqlDataAsync(sql);
        }
        
        public System.Threading.Tasks.Task<string> HWInterfaceAsync(string M_MACHINENO, string M_PRODUCT_SN, string M_MO, string M_EMP, string M_KP_ITEMSN_ALL)
        {
            return base.Channel.HWInterfaceAsync(M_MACHINENO, M_PRODUCT_SN, M_MO, M_EMP, M_KP_ITEMSN_ALL);
        }
        
        public System.Threading.Tasks.Task<string> GetWeightBeforeInjectionAAsync(string ProductSn)
        {
            return base.Channel.GetWeightBeforeInjectionAAsync(ProductSn);
        }
        
        public System.Threading.Tasks.Task<string> ExecuteNonQueryConnAsync(string sql, string Conn)
        {
            return base.Channel.ExecuteNonQueryConnAsync(sql, Conn);
        }
        
        public System.Threading.Tasks.Task<string> checkNetStatusAsync()
        {
            return base.Channel.checkNetStatusAsync();
        }
        
        public System.Threading.Tasks.Task<string> OfflineDataUploadAsync(string M_SN, string M_RESULT, string M_USERNO, string M_MACHINENO, string M_ERROR, string M_ITEMVALUE)
        {
            return base.Channel.OfflineDataUploadAsync(M_SN, M_RESULT, M_USERNO, M_MACHINENO, M_ERROR, M_ITEMVALUE);
        }
        
        public System.Threading.Tasks.Task<string> AddGongBoardAsync(string GroupInfo)
        {
            return base.Channel.AddGongBoardAsync(GroupInfo);
        }
        
        public System.Threading.Tasks.Task<string> AddDrillAsync(string GroupInfo)
        {
            return base.Channel.AddDrillAsync(GroupInfo);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.MESInterfaceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.MESInterfaceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.MESInterfaceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://10.210.88.98:8070/MESInterface.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.MESInterfaceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://10.210.88.98:8070/MESInterface.asmx");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            MESInterfaceSoap,
            
            MESInterfaceSoap12,
        }
    }
}