using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IServices.PDAIServices;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.Services.BASE;

namespace Blog.Core.Services.PDASeriveces
{
    public class CoatServices : BaseServices<CoatModel>, ICoatServices
    {
        ICoatRepository _dal;
        PMESWeb.MESInterfaceSoapClient pmes = new PMESWeb.MESInterfaceSoapClient(PMESWeb.MESInterfaceSoapClient.EndpointConfiguration.MESInterfaceSoap);
        IMapper _mapper;

        public CoatServices(IMapper mapper, ICoatRepository dal)
        {
            this._mapper = mapper;
            _dal = dal;
            this.BaseDal = dal;
        }
       /// <summary>
       /// 物料信息添加
       /// </summary>
       /// <returns></returns>
        public async Task<MessageModel<AgitationItems>> AddMaterial(AddMaterialParam model)
        {
            MessageModel<AgitationItems> message = new MessageModel<AgitationItems>();
            CoatModel result = new CoatModel();
            string[] ItemValueArray = model.StrMaterialnfor.Split('#');
            result = _mapper.Map<CoatModel>(model);
            result.StrMaterialNO = ItemValueArray[0];
            var mCount =  await _dal.MeterialByBom(result);
            if (mCount.Count<=0)
            {
                message.msg = result.StrMaterialNO+ "BOM中不存在该料号，请确认！";
                return message;
            }
            AgitationItems aiModel = new AgitationItems();
            aiModel.item_No = ItemValueArray[0];
            aiModel.item_Batch = ItemValueArray[1];
            aiModel.item_name = ItemValueArray[2];
            aiModel.item_Number = ItemValueArray[3];
            message.msg = "";
            message.success = true;
            message.response = aiModel;
            return message;
        }
        /// <summary>
        /// 关结
        /// </summary>
        /// <returns></returns>
        public async Task<MessageModel<string>> Close(CoatCloseParam model)
        {
            MessageModel<string> message = new MessageModel<string>();

            if (model.out_qty.ObjToInt()> model.input_sum.ObjToInt())
            {
                message.msg = "产出不允许大于投入";
                return message;
            }
            //if (model.shipment_no.Substring(0, 3) != "ZTD" && model.shipment_no.Substring(0, 3) != "FTD")
            //{
            //     message.msg = "该设备号不是单面涂布设备号，不允许操作！";
            //    return message;
            //}
            CoatModel cmodel = _mapper.Map<CoatModel>(model);
            bool blclose = await _dal.Close(cmodel);
            if (!blclose)
            {
                message.msg = "关结失败:参数"+model;
                return message;
            }
            message.success = true;
            message.msg = "关结成功";
            return message;
        }
        /// <summary>
        /// 设备基础信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MessageModel<DeviceChangedReturns>> DeviceChanged(DeviceChangedParam model)
        {
            MessageModel<DeviceChangedReturns> message = new MessageModel<DeviceChangedReturns>();
            DeviceChangedReturns result = new DeviceChangedReturns();
            var dcModel =  await _dal.DeviceChanged(model);
            result =  _mapper.Map<DeviceChangedReturns>(dcModel);

            if (result==null)
            {
                message.msg = "";
                message.response = result;
                return message;
            }
            var moModel =   await _dal.MeterialOverdue(dcModel);
           // result = _mapper.Map<DeviceChangedReturns>(moModel);
            result.out_qty = moModel.out_qty;
            //result.technology = moModel.technology;
            //result.out_qty = moModel.out_qty;
            var scnModel = await _dal.ShipCartNumber(dcModel);
            result.qty = (moModel.out_qty.ObjToInt() - scnModel.qty.ObjToInt()).ToString();
           
            message.msg = "";
            message.success = true;
            message.response = result;
            return message;
        }
        /// <summary>
        /// 上节点出货牌
        /// </summary>
        /// <returns></returns>
        public async Task<MessageModel<OldShipCartChangedReturns>> OldShipCartChanged(OldShipCartChangedParam model)
        {
            MessageModel<OldShipCartChangedReturns> message = new MessageModel<OldShipCartChangedReturns>();
            OldShipCartChangedReturns result = new OldShipCartChangedReturns();
            CoatModel cmModel = _mapper.Map<CoatModel>(model);
            var moModel = await _dal.MeterialOverdue(cmModel);
            if (moModel==null)
            {
                message.success = false;
                message.msg = "亲，查无该出货牌号码，请确认！";
                return message;
            }
            if (string.IsNullOrEmpty(moModel.timeDifference) || moModel.timeDifference.ObjToInt() > 0)
            {
                message.msg = "亲，浆料已超期，请处理！";
                return message;
            }
            if (!model.checkPermission)
            {
                if (moModel.technology != model.technology)
                {
                    message.msg = "出货牌Package与制令单号不一致！";
                    return message;
                }
             }
            result.out_qty = moModel.out_qty;
            var snModel =  await _dal.ShipCartNumber(cmModel);
            if (string.IsNullOrEmpty(snModel.qty))
            {
                snModel.qty = "0";
            }
            result.sY_QTY = (moModel.out_qty.ObjToInt() - snModel.qty.ObjToInt()).ToString();
            if (result.sY_QTY == "0")
            {
                message.msg = "上节点出货牌数量已用完";
                return message; 
            }
            result.engineer = moModel.engineer;
            result.technology = moModel.technology;
            message.success = true;
            message.msg = "";
            message.response = result;
            return message;
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public async Task<MessageModel<string>> Submit(CoatSubmitParam model)
        {
            model.group_name = model.group_name.ToUpper();
            MessageModel<string> message = new MessageModel<string>();
            CoatModel cmodel = _mapper.Map<CoatModel>(model);
            //添加机台使用状况
            if (string.IsNullOrEmpty(cmodel.shipment_no))
            {
                message.msg="出货牌号不允许为空";
                return message;
            }
            if (string.IsNullOrEmpty(cmodel.input_sum) || cmodel.input_sum == "0")
            {
                message.msg = "投入数量为0或为空";
                return message;
            }
            if (string.IsNullOrEmpty(cmodel.user_name))
            {
                message.msg = "请输入操作员";
                return message;
            }
            if (string.IsNullOrEmpty(cmodel.monitor))
            {
                message.msg = "请输入监控员！";
                return message;
            }
            if (string.IsNullOrEmpty(cmodel.group_name))
            {
                message.msg = "请输入组别！";
                return message;
            }
            if (cmodel.productType != "正极辊压" && cmodel.productType != "负极辊压"&& cmodel.productType != "正极分条" && cmodel.productType != "负极分条" && cmodel.productType != "正极模切" && cmodel.productType != "负极模切"&& cmodel.productType != "正极裁片" && cmodel.productType != "负极裁片") {
                if (model.itemList == null || model.itemList.Count <= 0)
                {
                    message.msg = "请输入物料信息";
                    return message;
                }
               
            }
            if (cmodel.productType == "正极分条" || cmodel.productType == "负极分条")
            {
                if (string.IsNullOrEmpty(cmodel.Partition_Sum)|| cmodel.Partition_Sum=="0")
                {
                    message.msg = "分条数不能为空或者为空0";
                    return message;
                }
            }


            if (!model.checkPermission)
            {
                //组别校验
                string strGroup = await pmes.GroupNameCheckAsync(cmodel.previous_shipment_no + ":" + cmodel.shipment_no, cmodel.group_name, cmodel.technology, cmodel.productType, cmodel.user_name);
                if (strGroup.ToUpper() != "TRUE")
                {
                    message.msg = strGroup;
                    return message;
                }
            }
            else
            {
                //记录组别
                await _dal.AddGroupName(cmodel);
            }
            var gsdata =  await _dal.GetShipcart(cmodel);
            if (gsdata.Count >0)
            {
                message.msg = "出货批号已存在";
                return message;
            }
            if (cmodel.input_sum.ObjToInt() > cmodel.out_qty.ObjToInt())
            {
                message.msg = "投入量不能大于剩余量";
                return message;
            }
            string strItem = string.Empty;
            if (cmodel.productType == "正极单面涂布")
            {
                strItem = "正极双面涂布";
            }
            else if (cmodel.productType == "正极双面涂布")
            {
                strItem = "正极辊压";
            }
            else if (cmodel.productType == "正极辊压")
            {
                strItem = "正极分条";
            }
            else if (cmodel.productType == "正极分条")
            {
                strItem = "正极模切";
            }
            else if (cmodel.productType == "正极模切")
            {
                strItem = "正极裁片";
            }
            else if (cmodel.productType == "正极裁片")
            {
                strItem = "结束";
            }
            else if (cmodel.productType == "负极单面涂布")
            {
                strItem = "负极双面涂布";
            }
            else if (cmodel.productType == "负极双面涂布")
            {
                strItem = "负极辊压";
            }
            else if (cmodel.productType == "负极辊压")
            {
                strItem = "负极分条";
            }
            else if (cmodel.productType == "负极分条")
            {
                strItem = "负极模切";
            }
            else if (cmodel.productType == "负极模切")
            {
                strItem = "负极裁片";
            }
            else if (cmodel.productType == "负极裁片")
            {
                strItem = "结束";
            }

            var momodel =  await _dal.MeterialOverdue(cmodel);
            cmodel.type_name = momodel.type_name;
            cmodel.package_id = momodel.package_id;
            cmodel.strItem = strItem;
            
            
            if (cmodel.productType == "正极单面涂布" || cmodel.productType == "负极单面涂布" || cmodel.productType == "正极双面涂布" || cmodel.productType == "负极双面涂布")
            {
                if (!string.IsNullOrEmpty(cmodel.previous_shipment_no))
                {
                    cmodel.itemName = "浆料批号";
                    cmodel.itemValue = cmodel.previous_shipment_no;
                    await _dal.AddMetrial(cmodel);
                }
            }
            if (cmodel.productType == "正极辊压" || cmodel.productType == "负极辊压"|| cmodel.productType == "正极模切" || cmodel.productType == "负极模切" || cmodel.productType == "正极裁片" || cmodel.productType == "负极裁片")
            {
                if (!string.IsNullOrEmpty(cmodel.previous_shipment_no))
                {
                    cmodel.itemName = "卷料批号";
                    cmodel.itemValue = cmodel.ItemName;
                    await _dal.AddMetrial(cmodel);
                }
                if (!string.IsNullOrEmpty(cmodel.Partition_XY))
                {
                    cmodel.itemName = "X或Y";
                    cmodel.itemValue = cmodel.Partition_XY;
                    await _dal.AddMetrial(cmodel);
                }
            }
            else if (cmodel.productType == "正极分条" || cmodel.productType == "负极分条")
            {
                cmodel.itemName = "分条数";
                cmodel.itemValue = cmodel.Partition_Sum;
                await _dal.AddMetrial(cmodel);
                cmodel.itemName = "X或Y";
                cmodel.itemValue = cmodel.Partition_XY;
                await _dal.AddMetrial(cmodel);
                double Input_Sum = Convert.ToDouble(cmodel.input_sum) / Convert.ToDouble(cmodel.Partition_Sum);
                cmodel.input_sum = Math.Floor(Input_Sum).ToString();
                for (int  i = 1; i <= cmodel.Partition_Sum.ObjToInt(); i++)
                {
                    if (i != 1)
                    {
                        GetShipCartCoatParam getshipCartModel = new GetShipCartCoatParam();
                        getshipCartModel = _mapper.Map<GetShipCartCoatParam>(cmodel);
                        MessageModel < GetShipCartCoatReturns > getShipCartReturn = await this.GetShipCartCoat(getshipCartModel,i);
                        cmodel.shipment_no = getShipCartReturn.response.shipment_no;
                    }
                    await _dal.AddShipcart(cmodel);
                    await _dal.UpShipcart(cmodel);
                }
                message.success = true;
                message.msg = "添加成功";
                return message;
            }
            else
            {
                string materstr = string.Empty;
                foreach (var item in model.itemList)
                {
                    materstr += "|" + item.item_No + ":" + item.item_Batch + ";0";
                }

                //先添加，再校验物料
                string strGT = await pmes.CellBOMItemsCheckAsync(cmodel.shipment_no, cmodel.technology, cmodel.user_name, cmodel.device_no, materstr);
                if (strGT.ToUpper() != "TRUE")
                {
                    message.msg = strGT;
                    //校验失败，删除出货牌
                    await _dal.DeteleShipcart(cmodel);
                    return message;
                }
                foreach (var item in model.itemList)
                {
                    cmodel.itemName = item.item_name;
                    cmodel.itemValue = item.item_No + "|" + item.item_Batch;
                    await _dal.AddMetrial(cmodel);
                }
            }
            await _dal.AddShipcart(cmodel);
            await _dal.UpShipcart(cmodel);
            message.success = true;
            message.msg = "添加成功";
            return message;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public async Task<MessageModel<string>> UpdateShipCart(CoatUpdateParam model)
        {
            MessageModel<string> message = new MessageModel<string>();

            CoatModel cmodel = _mapper.Map<CoatModel>(model);
           bool bl =   await _dal.CoatUpdate(cmodel);
            cmodel.itemName = "追加投入数量";
            cmodel.itemValue = model.shipCard_update+"|"+cmodel.input_sum;
           bool bladd =  await _dal.AddMetrial(cmodel);
            if (!bl || !bladd)
            {
                message.msg = "更新失败:"+model;
                return message;
            }
            message.success = true;
            message.msg = "";
            return message;
        }
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <returns></returns>
        public async Task<MessageModel<CheckUserRoleReturns>> CheckUserRole(CheckUserRoleparam model)
        {
            MessageModel<CheckUserRoleReturns> message = new MessageModel<CheckUserRoleReturns>();
            CheckUserRoleReturns result = new CheckUserRoleReturns();
            //验证权限
            string res = await pmes.CheckUserRoleAsync(model.username,model.password, "跨工单投入");
            if (res.ToUpper() != "TRUE")
            {
                message.msg = res;
                return message;
            }
            result.checkPermission = true;
            await _dal.CheckUserRole(model);
            message.success = true;
            message.msg = "校验成功";
            message.response = result;
            return message;
        }
        /// <summary>
        ///  新增需求区分出货牌AB卷
        /// </summary>
        private enum AzCode
        {
            A = 1,
            B = 2,
            C = 3,
            D = 4,
            E = 5,
            F = 6,
            G = 7,
            H = 8,
            I = 9,
            J = 10,
            K = 11,
            L = 12,
            M = 13,
            N = 14,
            O = 15,
            P = 16,
            Q = 17,
            R = 18,
            S = 19,
            T = 20,
            U = 21,
            V = 22,
            W = 23,
            X = 24,
            Y = 25,
            Z = 26,
        }

        /// <summary>
        /// 获取出货牌
        /// </summary>
        /// <returns></returns>
        public async Task<MessageModel<GetShipCartCoatReturns>> GetShipCartCoat(GetShipCartCoatParam model, int isum=1)
        {
            MessageModel<GetShipCartCoatReturns> message = new MessageModel<GetShipCartCoatReturns>();
            model.group_name = model.group_name.ToUpper();
            string strCode = string.Empty;
            if (model.productType == "正极单面涂布")
            {
                strCode = "ZTD";
            }
            else if (model.productType == "正极双面涂布")
            {
                strCode = "ZTS";
            }
            else if (model.productType == "正极辊压")
            {
                strCode = "ZG";
            }
            else if (model.productType == "正极分条")
            {
                strCode = "ZF";
            }
            else if (model.productType == "正极模切")
            {
                strCode = "ZM";
            }
            else if (model.productType == "正极裁片")
            {
                strCode = "ZC";
            }
            else if (model.productType == "负极单面涂布")
            {
                strCode = "FTD";
            }
            else if (model.productType == "负极双面涂布")
            {
                strCode = "FTS";
            }
            else if (model.productType == "负极辊压")
            {
                strCode = "FG";
            }
            else if (model.productType == "负极分条")
            {
                strCode = "FF";
            }
            else if (model.productType == "负极模切")
            {
                strCode = "FM";
            }
            else if (model.productType == "负极裁片")
            {
                strCode = "FC";
            }
            CoatModel cmmodel = _mapper.Map<CoatModel>(model);
            var  scnCount =  await _dal.ShipCartNoClose(cmmodel);
            if (scnCount.Count > 0)
            {
                message.msg = "该机器编号存在未关节的出货牌";
                return message;
            }
           var gscmodel =  await _dal.GetShipCartNo(cmmodel);
            if (gscmodel != null)
            {
                gscmodel.sys_Date = gscmodel.sys_Date.Replace("/", "");
                if (gscmodel.s_date == gscmodel.sys_Date)
                {
                    gscmodel.s_date = gscmodel.s_date.Replace("/", "");
                    gscmodel.strLSH = (gscmodel.s_number.TrimStart('0').ObjToInt() + 1).ToString().PadLeft(2, '0');
                    gscmodel.shipment_no = strCode + gscmodel.s_date + model.technology+ model.group_name + gscmodel.strLSH;
                }
                else
                {
                    gscmodel.strLSH = "01";
                    gscmodel.shipment_no = strCode + gscmodel.sys_Date + model.technology + model.group_name + gscmodel.strLSH;
                }
                gscmodel.productType = model.productType;
                //新增需求针对分条工序，根据分条数来区分AB卷，在原来的规则后面追加标识字符A~Z
                if (model.productType == "正极分条"|| model.productType == "负极分条")
                {
                    gscmodel.shipment_no += (AzCode)isum;
                }
                await _dal.UpdateShipCartNo(gscmodel);
            }
            else
            {
                await _dal.AddShipCartNo(gscmodel);

                await GetShipCartCoat(model);
            }
            GetShipCartCoatReturns result   = _mapper.Map<GetShipCartCoatReturns>(gscmodel);
            message.success = true;
            message.msg = "";
            message.response = result;
            return message;
        }
    }
}
