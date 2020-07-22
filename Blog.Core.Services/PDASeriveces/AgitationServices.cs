using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IServices.PDAIServices;
using Blog.Core.Log;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.Services.BASE;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.Core.Services.PDASeriveces
{
    public class AgitationServices : BaseServices<t_gule_step_info>, IAgitationServices
    {
        IAgitationRepository _dal;
        PMESWeb.MESInterfaceSoapClient pmes = new PMESWeb.MESInterfaceSoapClient(PMESWeb.MESInterfaceSoapClient.EndpointConfiguration.MESInterfaceSoap);
        IMapper _mapper;
        private readonly ILoggerHelper _loggerHelper;


        public AgitationServices(IMapper mapper, IAgitationRepository dal, ILoggerHelper loggerHelper)
        {
            this._mapper = mapper;
            _dal = dal;
            this.BaseDal = dal;
            _loggerHelper = loggerHelper;
        }
        /// <summary>
        /// 关结
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MessageModel<string>> Close(CloseParam model)
        {
            string[] currenStep = { "正极打胶", "正极搅拌", "正极单面涂布", "正极双面涂布", "正极辊压", "正极分条", "正极模切", "正极裁片", "负极打胶", "负极搅拌", "负极单面涂布", "负极双面涂布", "负极辊压", "负极分条", "负极模切", "负极裁片" };
            
            MessageModel<string> message = new MessageModel<string>();
            //if (model.device_sn.Substring(0, 2) != "ZJ" && model.device_sn.Substring(0, 2) != "FJ")
            //{
            //    message.msg =  model.device_sn.Substring(0, 2) + "该设备号不符合规范,命名必须是ZJ或FJ开头，不允许操作！";
            //    return message;
            //}
            if (model.out_qty.ObjToInt() > model.input_Sum.ObjToInt())
            {
                message.msg = "出货数量不能大于投入数量";
                return message;
            }
            if (string.IsNullOrEmpty(model.out_qty) || model.out_qty.ObjToInt()<=0)
            {
                message.msg = "请输入有效的出货数量";
                return message;
            }
            //查找下一步骤名称
           int currenNumber = Array.IndexOf(currenStep, model.productType);
            string nextStep = currenStep[currenNumber + 1];
            AgitationModel dbModel = new AgitationModel();
            dbModel = _mapper.Map<AgitationModel>(model);
            dbModel.productItem = nextStep;

           await   _dal.UpdateShipCartState(dbModel);
           await _dal.wip_QTY(model);
            message.success = true;
            message.msg = "关结成功";
            await _dal.wip_QTY( model);
            return message;
        }
        /// <summary>
        /// 设备文本选择监听事件
        /// </summary>
        /// <param name="model">{device_sn}</param>
        /// <returns>[]</returns>
        public async Task<DeviceSelectReturns> DeviceSelect(DeviceSelectParam model)
        {
            DeviceSelectReturns dr = new DeviceSelectReturns();
            string strLookMH = string.Format(@"SELECT T.PACKAGE_ID,T.TECHNOLOGY,T.ENGINEER,T.GROUP_NAME,T.SHIPMENT_NO,T.FORMULA_NO,T.SOLID_CONTENT,T.REQUIREMENT,T.INPUT_SUM ,T.STIR_SUM,T.OUT_QTY,T.USER_NAME,T.TYPE_NAME from T_MES_MACHINE_USEINFO t where t.device_no='{0}'  and t.statue=0", model.device_sn);
            //var res = await base.SqlQuery<DeviceSelectReturns>(strLookMH);
            var res = (await pmes.GetSqlDataAsync(strLookMH)).DataToXml<DeviceSelectReturns>();

            if (res.Count > 0)
            {
                //获取组别相关的Package列表
                GroupKeyPressParam getPackges = new GroupKeyPressParam() { groupName = res[0].GROUP_NAME, moNumber = res[0].TECHNOLOGY, productType = model.productType };
                res[0].PACKAGE_IDS = (await _dal.GetLook(getPackges)).SelectByItem<AgitationModel, GroupKeyPressReturns>(m => m.package_id, m => m.packagenumber, m => m.cell_model, m => m.engineer).FindAll(m => m.packagenumber == res[0].TECHNOLOGY);
                return res[0];
            }
            else return dr;
        }
        
        /// <summary>
        /// 获取出货牌
        /// </summary>
        /// <param name="model">{productType,device_sn}</param>
        /// <returns></returns>
        public async Task<MessageModel<GetShipCardReturns>> GetShipCarts(GetShipCardParam model)
        {
            MessageModel<GetShipCardReturns> message = new MessageModel<GetShipCardReturns>();
            GetShipCardReturns Rsultmode = new GetShipCardReturns();
            string productCode = string.Empty;
            //if (model.productType == "正极搅拌")
            //{
            //    productCode = "ZJ";
            //}
            //if (model.productType == "负极搅拌")
            //{
            //    productCode = "FJ";
            //}
            productCode = model.device_sn.Substring(0, 3);
           var result =  await _dal.GetSerialnumber(model);
            if (result != null)
            {
                int intLenght = model.device_sn.Length;
                result.sys_Date = result.sys_Date.Replace("/", "");
                result.strLSH = string.Empty;
                if (result.s_date == result.sys_Date)
                {
                    result.s_date = result.s_date.Replace("/", "");
                    result.strLSH = (Int32.Parse(result.s_number) + 1).ToString().PadLeft(2, '0');
                    result.shipment_no = productCode + result.s_date +model.package+model.formula+ model.device_sn.Substring(intLenght - 2, 2) + result.strLSH;
                }
                else
                {
                    result.strLSH = "01";
                    result.shipment_no = productCode + result.sys_Date + model.package + model.formula + model.device_sn.Substring(intLenght - 2, 2) + result.strLSH;
                }
                result.productType = model.productType;
                await _dal.UpdateShipcartCode(result);
                Rsultmode = _mapper.Map<GetShipCardReturns>(result);
                message.success = true;
                message.msg = "";
                message.response = Rsultmode;
            }
            else
            {
                message.success = false;
                message.msg = "获取出货牌失败";
            }
            return message;
           
        }
        /// <summary>
        /// 组别回车监听事件
        /// </summary>
        /// <param name="model">{moNumber,groupName,productType}</param>
        /// <returns>{}</returns>
        public async Task<MessageModel<List<GroupKeyPressReturns>>> GroupKeyPress(GroupKeyPressParam model)
        {
            MessageModel<List<GroupKeyPressReturns>> message = new MessageModel<List<GroupKeyPressReturns>>();

            message.msg = "查无该Package计划，请确认！";
            message.success = false;
           var result = ( await _dal.GetLook(model)).SelectByItem<AgitationModel, GroupKeyPressReturns>(m=>m.package_id,m=>m.packagenumber,m=>m.engineer,m=>m.cell_model);
            if (result.Count > 0)
            {
                message.success = true;
                message.msg = "";
                message.response = result;
            }
            return message;
        }
        /// <summary>
        /// 页面信息加载
        /// </summary>
        /// <param name="model">device_sn,work_stationname</param>
        /// <returns>device_sns[]</returns>
        public async Task<MessageModel<List<AgitationModel>>> Load(AgitationLoad model)
        {
            MessageModel<List<AgitationModel>> message = new MessageModel<List<AgitationModel>>();

            var result =   await  _dal.GlueLoad(model);
            if (result.Count <= 0) return message;
            if (result[0].work_stationname != model.work_stationname)
            {
                message.success = false;
                message.msg = $"该设备号不是【{model.work_stationname}】设备号,不允许操作";
                return message;
            }
            message.success = true;
            message.msg = "";
            message.response = result;
            return message;
        }
        /// <summary>
        /// 弃用方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<MessageModel<string>> PackageSelect(AgitationModel model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MessageModel<AgitationSubmitParam>> Submit(AgitationSubmitParam model)
        {
            MessageModel<AgitationSubmitParam> message = new MessageModel<AgitationSubmitParam>();
            try
            {
                model.stir_sum = Math.Round(Convert.ToDouble(model.stir_sum), 1).ToString();
                await _dal.InsertSubmit(model);
                await _dal.wip_QTY(model);
                await _dal.InserGroup(model);
                await _dal.UpdatePackageState(model);
                message.success = true;
                message.msg = "添加成功";
                message.response = model;
            }
            catch (Exception ex)
            {
                message.success = false;
                message.msg = "添加失败-"+ ex;
            }
            return message;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">{shipment_no}</param>
        /// <returns></returns>
        public async Task<MessageModel<AgitationUpdateShipCartParam>> UpdateShipCart(AgitationUpdateShipCartParam model)
        {
            MessageModel<AgitationUpdateShipCartParam> message = new MessageModel<AgitationUpdateShipCartParam>();
            if (string.IsNullOrEmpty(model.solid_content))
            {
                message.success = false;
                message.msg = "固含量不能为空,请重新输入";
                return message;
            }
            if (string.IsNullOrEmpty(model.viscosity))
            {
                message.success = false;
                message.msg = "调节粘度不能为空,请重新输入";
                return message;
            }
            var result =  await _dal.GetShipCartInfo(model);
            if (result != null)
            {
                //更新固含量
                if (!string.IsNullOrEmpty(model.shipment_no))
                {
                    await _dal.UpdateShipCartBySolid_content(model);
                }
                //更新调节粘度
                if (!string.IsNullOrEmpty(model.viscosity))
                {
                    await _dal.InsertShipCartinfo(model);
                }
                message.msg = "更新成功！";
                message.success = true;
                message.response = model;
            }
            else
            {
                message.msg = "出货牌不存在";
            }
            return message;
        }
        /// <summary>
        ///获取全部出货牌信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<DeviceSelectReturns>> GetShipCartAll(DeviceSelectReturns model  )
        {
            string strLookMH = string.Format(@"select t.shipment_no,t.package_id from T_MES_MACHINE_USEINFO t where t.TECHNOLOGY='{0}'", model.TECHNOLOGY);
            var res = (await pmes.GetSqlDataAsync(strLookMH)).DataToXml<DeviceSelectReturns>();
            return res;
        }
        #region 搅拌详细页
        /// <summary>
        /// 当前步骤获取基础信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MessageModel<AgitationStrstepNameSelecteReturns>> StrstepNameSelecte(AgitationStrstepNameSelecteParam model)
        {
            MessageModel<AgitationStrstepNameSelecteReturns> message = new MessageModel<AgitationStrstepNameSelecteReturns>();
            AgitationStrstepNameSelecteReturns reModel = new AgitationStrstepNameSelecteReturns();
            var result = await _dal.StrstepNameSelecte(model);
            if (result.Count > 0)
            {
                reModel.MONITOR = result[0].MONITOR;
                reModel.USERNAME = result[0].USERNAME;
                reModel.next_step = result[0].next_step;
                int j = 0;
                foreach (var item in result)
                {
                    string StrItem_Name = item.item_name;
                    string StrItem_Value = item.item_value;
                    if (StrItem_Name == "公转转速")
                    {
                        reModel.publicSpeed = StrItem_Value;
                    }
                    else if (StrItem_Name == "自转转速")
                    {
                        reModel.selfVelocity = StrItem_Value;
                    }
                    else if (StrItem_Name == "搅拌时间")
                    {
                        reModel.stirTime = StrItem_Value;
                    }
                    else if (StrItem_Name == "温度")
                    {
                        reModel.temperature = StrItem_Value;
                    }
                    else if (StrItem_Name == "真空度")
                    {
                        reModel.vacuum = StrItem_Value;
                    }
                    else if (StrItem_Name == "粘度")
                    {
                        reModel.viscosity = StrItem_Value;
                    }
                    else if (StrItem_Name == "细度")
                    {
                        reModel.fineness = StrItem_Value;
                    }
                    else if (StrItem_Name == "过筛况")
                    {
                        reModel.filter = StrItem_Value;
                    }
                    else if (StrItem_Name == "除铁要求")
                    {
                        reModel.chutie = StrItem_Value;
                    }
                    else
                    {
                        //添加物料信息数据
                        string[] ItemValueArray = StrItem_Value.Split('|');
                        AgitationItems items = new AgitationItems();
                        items.item_name = item.item_name;
                        items.item_Batch = ItemValueArray[0];
                        items.item_No = ItemValueArray[1];
                        items.item_Number = item.ITEM_WEIGHT;
                        reModel.items.Add(items);
                    }
                }
                //重复的步骤可以重复输入
                if (result[0].current_steps == result[0].next_step)
                {
                    message.msg = "";
                    message.response = reModel;
                    message.success = true;
                    return message;
                }
                message.success = true;
                message.msg = "";
                message.response = reModel;
            }
            else
            {
                message.msg = "";
                message.response = reModel;
            }
            return message;
        }
        /// <summary>
        /// 物料信息添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MessageModel<AgitationItems>> GetItemInfo(AgitationGetItemInfoParam model)
        {
            
            MessageModel<AgitationItems> message = new MessageModel<AgitationItems>();
            if (string.IsNullOrEmpty(model.stepModel.STEP_NAME))
            {
                message.msg = "请选择工序";
                return message;
            }
            AgitationItems result = new AgitationItems();
            try
            {
                string[] ItemValueArray = model.StrMaterialnfor.Split('#');
                result.item_No = ItemValueArray[0].Trim();
                result.item_Batch = ItemValueArray[1].Trim();
                result.item_Number = ItemValueArray[2].Trim();
                result.item_name = ItemValueArray[3].Trim();
                result.putNumber = model.StrMaterialNumber.Trim();
            }
            catch (Exception ex)
            {
                message.msg = "物料信息：物料号#物料批号#数量#名称,请确认！";
                return message;
            }
            




            //校验物料信息(惠州)
            model.stepModel.ITEM_CODE = result.item_No;
            model.stepModel.POWDER_MATERIAL = result.item_name;
            model.stepModel.POWDER_Value = model.StrMaterialNumber.Trim().ObjToFloat();
            bool isCheck = await _dal.CheckMartter(model.stepModel);
            if (!isCheck && model.stepModel.template)
            {
                message.success = false;
                message.msg = "物料信息校验出错，请确认物料是否维护或者超过上下限";
                return message;
            }

            if (result.item_name == "正极打胶" || result.item_name == "负极打胶")
            {
                var  data =  await _dal.GetTimeCheck(result);
                if (data != null)
                {
                    if (data.CMC > 0 || data.PVDF > 0)
                    {
                        if (data.timeDifference != null)
                        {
                            message.msg = "亲，PVDF/CMC胶液已超期，请处理！";
                            //message.response = result;
                            return message;
                        }
                    }
                }
                else
                {
                    message.msg = "亲，出货牌不存在哦，请确认！";
                    //message.response = result;
                    return message;
                }

            }
            message.msg = "";
            message.success = true;
            message.response = result;
            return message;
        }
        /// <summary>
        /// 提交信息
        /// </summary>
        /// <param name="model">{itemValue1,shipCard,stepName,device_no,next_step,USERNAME,MONITOR,ITEM_WEIGHT,itemList[{item_No,item_Batch,item_name}]}</param>
        /// <returns></returns>
        public async Task<MessageModel<AgitationDetailModel>> Submit(AgitationDetailParam model )
        {
           // Convert.ToInt64(model.device_no);
            _loggerHelper.Info("搅拌工步提交参数", JsonConvert.SerializeObject(model));
            MessageModel<AgitationDetailModel> message = new MessageModel<AgitationDetailModel>();

            //1.不是第一个工步则判断他上个工序有没有做
            if (model.STEP_NUMBER >1)
            {
                AgitationStrstepNameSelecteParam asns = new AgitationStrstepNameSelecteParam()
                {
                    shipCard = model.shipCard,
                    stepName = model.last_step,
                };
                var stepsns = await this.StrstepNameSelecte(asns);
                if (!stepsns.success)
                {
                    message.msg = "请完善前面的工序,不允许跨步骤";
                    return message;
                }

            }
            //拼接物料号
            var stepcount =   await _dal.GetStepNumber(model);
            AgitationDetailModel admModel = new AgitationDetailModel();
            admModel = _mapper.Map<AgitationDetailModel>(model);
            if (stepcount == null)
            {
                message.msg = "添加失败";
                message.response = admModel;
                return message;
            }

            //2.校验信息
            model.guleStepModel.STEP_NAME = model.stepName;
            var stepData = await _dal.GetStepListAllByStepName(model.guleStepModel);
            if (stepData == null)
            {
                message.success = false;
                message.msg = "您还没有维护此工步,请联系相关人员维护";
                return message;
            }    
            if(string.IsNullOrEmpty(admModel.publicSpeed))
            admModel.publicSpeed = null;
            if (string.IsNullOrEmpty(admModel.selfVelocity))
                admModel.selfVelocity = null;
            if (string.IsNullOrEmpty(admModel.vacuum))
                admModel.vacuum = null;
            if (string.IsNullOrEmpty(admModel.temperature))
                admModel.temperature = null;
            if (!string.IsNullOrEmpty(stepData.ITEM_CODE))
            {
                if (model.itemList.Count <= 0)
                {
                    message.msg = "此工步需要校验物料信息,请检查";
                    return message;
                }
            }
            //公转-搅拌速度
            if (!(admModel.publicSpeed.ObjToFloat()<=stepData.MIXING_SPEED_UPPER &&admModel.publicSpeed.ObjToFloat() >= stepData.MIXING_SPEED_LOWER))
                {
                    message.msg = "公转速度上下限：" + stepData.MIXING_SPEED_LOWER + "~" + stepData.MIXING_SPEED_UPPER;
                    return message;
                }
                //自转-分离速度
                if (!(admModel.selfVelocity.ObjToFloat() <= stepData.SEPARATE_UPPER && admModel.selfVelocity.ObjToFloat() >= stepData.SEPARATE_LOWER))
                {
                    message.msg = "自转速度上下限：" + stepData.SEPARATE_LOWER + "~" + stepData.SEPARATE_UPPER;
                    return message;
                }
            //真空度
                if (!(admModel.vacuum.ObjToFloat() <= stepData.VACUUM_UPPER ))
                {
                if (stepData.VACUUM_LOWER!=null && admModel.vacuum.ObjToFloat() >= stepData.VACUUM_LOWER.ObjToFloat()) 
                {
                    message.msg = "真空度上下限：" + stepData.VACUUM_LOWER.ObjToFloat() + "~" + stepData.VACUUM_UPPER;
                    return message;
                }
                    message.msg = "真空度上限：" + stepData.VACUUM_UPPER;
                    return message;
                }
          
                //温度
                if (!(admModel.temperature.ObjToFloat() <= stepData.TEMPER_UPPER && admModel.temperature.ObjToFloat() >= stepData.TEMPER_LOWER))
                {
                    message.msg = "温度上下限：" + stepData.TEMPER_LOWER + "~" + stepData.TEMPER_UPPER;
                    return message;
                }

            //3.工步是否已经录入，录入做修改，不录入新增
            var stepEntil = await this.StrstepNameSelecte(model.stepModel);
            int intStep = 0;
            if (stepEntil.success)
            {
                //删除原来，新增
                await _dal.DeleteStepInfo(model, model.stepModel);
                intStep = model.STEP_NUMBER + 1;
            }
            else
            {
                intStep = stepcount.step_num + 1;
            }
            //4.提交信息
            
            
            bool bl = false;
            string itemName = string.Empty;
            string itemValue = string.Empty;
            
            if (model.itemList.Count > 0)
            {
                foreach (var item in model.itemList)
                {
                    admModel.item_value = item.item_No.Trim() +"|"+ item.item_Batch.Trim()+"|"+ item.item_Number.Trim();
                    admModel.item_name = item.item_name.Trim();
                    admModel.item_number = item.putNumber;
                    //item.item_value = item.item_No + item.item_Batch;
                    await _dal.AddTestValue(intStep, admModel);
                }
                bl = true;
            }
            //else
            //{
            //    message.success = false;
            //    message.msg = "物料信息不能为空";
            //    message.response = admModel;
            //    return message;
            //}
            if (!string.IsNullOrEmpty(admModel.publicSpeed))
            {
                admModel.item_name = "公转转速";
                admModel.item_value = admModel.publicSpeed;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (!string.IsNullOrEmpty(admModel.selfVelocity))
            {
                admModel.item_name = "自转转速";
                admModel.item_value = admModel.selfVelocity;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (!string.IsNullOrEmpty(admModel.stirTime))
            {
                admModel.item_name = "搅拌时间";
                admModel.item_value = admModel.stirTime;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (!string.IsNullOrEmpty(admModel.temperature))
            {
                admModel.item_name = "温度";
                admModel.item_value = admModel.temperature;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (!string.IsNullOrEmpty(admModel.vacuum))
            {
                admModel.item_name = "真空度";
                admModel.item_value = admModel.vacuum;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (!string.IsNullOrEmpty(admModel.viscosity))
            {
                admModel.item_name = "粘度";
                admModel.item_value = admModel.viscosity;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (!string.IsNullOrEmpty(admModel.fineness))
            {
                admModel.item_name = "细度";
                admModel.item_value = admModel.fineness;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (!string.IsNullOrEmpty(admModel.filter))
            {
                admModel.item_name = "过筛况";
                admModel.item_value = admModel.filter;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (!string.IsNullOrEmpty(admModel.chutie))
            {
                admModel.item_name = "除铁要求";
                admModel.item_value = admModel.chutie;
                admModel.item_weight = "";
                await _dal.AddTestValue(intStep, admModel);
                bl = true;
            }
            if (bl)
            {
                await _dal.UpdateShipCartInfo(admModel);
                message.success = true;
                message.msg = "添加成功";
                message.response = admModel;
            }
            return message;
        }
        /// <summary>
        /// 获取工步名称列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  async Task<MessageModel<List<t_gule_step_infoVM>>> GetStepList(t_gule_step_infoVM model)
        {
            MessageModel<List<t_gule_step_infoVM>> message = new MessageModel<List<t_gule_step_infoVM>>();
            var result = await _dal.GetStepList(model);
            if (result.Count <=0) {
                message.success = false;
                message.msg = "获取信息失败";
                return message;
            }
            message.success = true;
            message.msg = "获取信息成功";
            message.response = result;
            return message;
        }

   


        #endregion
    }
}
