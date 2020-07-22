using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IServices.PDAIServices;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.Services.BASE;
using SqlSugar;

namespace Blog.Core.Services.PDASeriveces
{
    public class GlueServices : BaseServices<GlueModel>, IGlueServices
    {
        
        IGlueRepository _dal;
        PMESWeb.MESInterfaceSoapClient pmes = new PMESWeb.MESInterfaceSoapClient(PMESWeb.MESInterfaceSoapClient.EndpointConfiguration.MESInterfaceSoap);
        IMapper _mapper;
        public GlueServices( IMapper mapper, IGlueRepository dal)
        {
            this._mapper = mapper;
            _dal = dal;
            this.BaseDal = dal;
        }
        /// <summary>
        /// 打胶参数录入-当前步骤值改变事件
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<CurrenStepSelectChangesReturns> CurrenStepSelectChanges(CurrenStepSelectChangesParam jsondata)
        {
            //返回信息实体
            CurrenStepSelectChangesReturns modelReturns = new CurrenStepSelectChangesReturns();
           var result =   await _dal.StrstepNameSelecte(jsondata);
            if (result.Count > 0)
            {
                int j = 0;
                modelReturns.MONITOR = result[0].MONITOR;
                modelReturns.USERNAME = result[0].USERNAME;
                for (int i = 0; i < result.Count; i++)
                {
                    string StrItem_Name = result[i].item_name;
                    string StrItem_Value = result[i].item_value;
                    if (StrItem_Name == "公转转速")
                    {
                        modelReturns.PublicSpeed = StrItem_Value;
                    }
                    else if (StrItem_Name == "自转转速")
                    {
                        modelReturns.SelfVelocity = StrItem_Value;
                    }
                    else if (StrItem_Name == "搅拌时间")
                    {
                        modelReturns.Time = StrItem_Value;
                    }
                    else if (StrItem_Name == "温度")
                    {
                        modelReturns.Temperature = StrItem_Value;
                    }
                    else if (StrItem_Name == "真空度")
                    {
                        modelReturns.Vacuum = StrItem_Value;
                    }
                    else if (StrItem_Name == "粘度")
                    {
                        modelReturns.Viscosity = StrItem_Value;
                    }
                    else if (StrItem_Name == "细度")
                    {
                        modelReturns.Fineness = StrItem_Value;
                    }
                    else if (StrItem_Name == "过筛况")
                    {
                        modelReturns.Filter = StrItem_Value;
                    }
                    else if (StrItem_Name == "除铁要求")
                    {
                        modelReturns.chutie = StrItem_Value;
                    }
                    else
                    {
                        //3个项目名称、值、重量
                        if (j == 0)
                        {
                            j++;
                            modelReturns.ItemName1 = StrItem_Name;
                            modelReturns.ItemValue1 = StrItem_Value;
                            modelReturns.Weight1 = result[i].ITEM_WEIGHT;
                        }
                        else if (j == 1)
                        {
                            j++;
                            modelReturns.ItemName2 = StrItem_Name;
                            modelReturns.ItemValue2 = StrItem_Value;
                            modelReturns.Weight2 = result[i].ITEM_WEIGHT;
                        }
                        else if (j == 2)
                        {
                            j++;
                            modelReturns.ItemName3 = StrItem_Name;
                            modelReturns.ItemValue3 = StrItem_Value;
                            modelReturns.Weight3 = result[i].ITEM_WEIGHT;
                        }
                        else if (j == 3)
                        {
                            j++;
                            modelReturns.ItemName4 = StrItem_Name;
                            modelReturns.ItemValue4 = StrItem_Value;
                            modelReturns.Weight4 = result[i].ITEM_WEIGHT;
                        }
                    }
                }
            }
            return modelReturns;
        }

        /// <summary>
        /// 获取设备正在作业出货牌信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<List<DeviceSelectChangesReturns>> DeviceSelectChanges(GlueLoadParam jsondata)
        {
            return (await _dal.GetGlueModel(jsondata)).SelectByItem<GlueModel, DeviceSelectChangesReturns>(m => m.shipCard, m => m.input_sum, m => m.outQTY, m => m.USER_NAME, m => m.item_value);
        }
        /// <summary>
        /// 打胶参数录入-页面加载
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<GlueDetailModel> GetCurrentStepLoad(GlueDetailParam jsondata)
        {
           return   await _dal.GetCurrentStep(jsondata);
        }

        /// <summary>
        /// 获取出货牌
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<MessageModel<GetShipCardReturns>> GetShipCard(GetShipCardParam jsondata)
        {
            MessageModel<GetShipCardReturns> message = new MessageModel<GetShipCardReturns>();
            GetShipCardReturns model = new GetShipCardReturns();
            string productCode = string.Empty;
            //if (jsondata.productType == "正极打胶")
            //{
            //    productCode = "ZD";
            //}
            //if(jsondata.productType =="负极打胶")
            //{
            //    productCode = "FD";
            //}
            productCode = jsondata.device_sn.Substring(0, 3);
            GlueModel fistResult =  await _dal.GetSerialnumber(jsondata);
           
            //fistResult = _mapper.Map<GlueModel>(jsondata);
            if (fistResult != null)
            {
                fistResult.sys_Date = fistResult.sys_Date.Replace("/", "");
                int intDevice = jsondata.device_sn.Length;
                if (fistResult.s_date == fistResult.sys_Date)
                {
                    fistResult.s_date = fistResult.s_date.Replace("/", "");
                    fistResult.strLSH = (fistResult.s_number.TrimStart('0').ObjToInt()+ 1).ToString().PadLeft(2, '0');
                    fistResult.shipCard = productCode + fistResult.s_date + jsondata.device_sn.Substring(intDevice - 2, 2) + fistResult.strLSH;
                }
                else
                {
                    fistResult.strLSH = "01";
                    fistResult.shipCard = productCode + fistResult.sys_Date + jsondata.device_sn.Substring(intDevice - 2, 2) + fistResult.strLSH;
                }
                GlueSubmitParam gpModel = new GlueSubmitParam();
                gpModel = _mapper.Map<GlueSubmitParam>(fistResult);
                await _dal.UpdateShipcartCode(gpModel);
            }
            else
            {
               await  _dal.InsertSerialnumber(jsondata);
                
                await GetShipCard(jsondata);
            }
            model = _mapper.Map<GetShipCardReturns>(fistResult);
            message.success = true;
            message.msg = "";
            message.response = model;
            return message;
        }
        /// <summary>
        /// 打胶关结
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<MessageModel<string>> GlueClose(GlueCloseParam jsondata)
        {
            MessageModel<string> message = new MessageModel<string>();
            try
            {
                //更新出货牌状态
                await _dal.UpdateShipCartState(jsondata);
                await _dal.wip_QTY( jsondata);
                message.success = true;
                message.msg = "关结成功";
            }
            catch (Exception ex)
            {
                message.success = false;
                message.msg = "关结失败";
            }
            return message;
        }
        /// <summary>
        ///  打胶加载页面获取对应设备信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<MessageModel<List<GlueLoadReturns>>> GlueLoad(GlueLoadParam jsondata)
        {
            MessageModel<List<GlueLoadReturns>> message = new MessageModel<List<GlueLoadReturns>>();
            message.response = (await _dal.GlueLoad(jsondata)).SelectByItem<GlueModel, GlueLoadReturns>(m => m.device_sn);
            message.success = true;
            message.msg = "";
            return message;
        }

        /// <summary>
        /// 打胶信息提交
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<MessageModel<GlueSubmitRaturns>> GlueSubmit(GlueSubmitParam jsondata)
        {
            MessageModel<GlueSubmitRaturns> message = new MessageModel<GlueSubmitRaturns>();
            GlueSubmitRaturns resultModel = new GlueSubmitRaturns();
            try
            {
                //更新出货牌流水号
                //await _dal.UpdateShipcartCode(jsondata);
                //增加出货牌
                await _dal.InsertShipcart(jsondata);
                await _dal.wip_QTY(jsondata);
                if (jsondata.solutions_Name != null)
                await _dal.AddTestValue("溶液名称",jsondata);
                if (jsondata.cmbSolutions_name != null)
                {
                    jsondata.solutions_Name = jsondata.cmbSolutions_name;
                    await _dal.AddTestValue("溶液名称", jsondata);
                }
                message.success = true;
                message.msg = "添加成功";
            }
            
            catch (Exception ex)
            {
                message.success = false;
                message.msg = "添加失败";
            }
            resultModel = _mapper.Map<GlueSubmitRaturns>(jsondata);
            message.response = resultModel;
            return message;
        }
        /// <summary>
        /// 打胶参数录入-提交
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public  async Task<MessageModel<string>> SubmitGlue(GlueDetailModel jsondata)
        {
            MessageModel<string> message = new MessageModel<string>() { success = false, msg = "服务器异常" };
            //没有出货牌，不能添加
            var model =  await _dal.GetShipCart(jsondata);
            if (model == null)
            {
                message.msg = "添加失败";
                return message;
            }
            int intStep = model.step_num.ObjToInt() + 1;
            bool bl = false;

            if (!string.IsNullOrEmpty(jsondata.ItemName1) && !string.IsNullOrEmpty(jsondata.ItemValue1) && !string.IsNullOrEmpty(jsondata.Weight1))
            {
                jsondata.item_name = jsondata.ItemName1;
                jsondata.item_value = jsondata.ItemValue1;
                jsondata.ITEM_WEIGHT = jsondata.Weight1;
                await _dal.AddTestValue(intStep,jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.ItemName2) && !string.IsNullOrEmpty(jsondata.ItemValue2) && !string.IsNullOrEmpty(jsondata.Weight2))
            {
                jsondata.item_name = jsondata.ItemName2;
                jsondata.item_value = jsondata.ItemValue2;
                jsondata.ITEM_WEIGHT = jsondata.Weight2;
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.ItemName3) && !string.IsNullOrEmpty(jsondata.ItemValue3) && !string.IsNullOrEmpty(jsondata.Weight3))
            {
                jsondata.item_name = jsondata.ItemName3;
                jsondata.item_value = jsondata.ItemValue3;
                jsondata.ITEM_WEIGHT = jsondata.Weight3;
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.ItemName4) && !string.IsNullOrEmpty(jsondata.ItemValue4) && !string.IsNullOrEmpty(jsondata.ItemName4))
            {
                jsondata.item_name = jsondata.ItemName4;
                jsondata.item_value = jsondata.ItemValue4;
                jsondata.ITEM_WEIGHT = jsondata.ItemName4;
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.PublicSpeed))
            {
                jsondata.item_name = "公转转速";
                jsondata.item_value = jsondata.PublicSpeed;
                jsondata.ITEM_WEIGHT = "";
                await  _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.SelfVelocity))
            {
                jsondata.item_name = "自转转速";
                jsondata.item_value = jsondata.SelfVelocity;
                jsondata.ITEM_WEIGHT = "";
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            
            if (!string.IsNullOrEmpty(jsondata.Time))
            {
                jsondata.item_name = "搅拌时间";
                jsondata.item_value = jsondata.Time;
                jsondata.ITEM_WEIGHT = "";
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.Temperature))
            {
                jsondata.item_name = "温度";
                jsondata.item_value = jsondata.Temperature;
                jsondata.ITEM_WEIGHT = "";
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.Vacuum))
            {
                jsondata.item_name = "真空度";
                jsondata.item_value = jsondata.Vacuum;
                jsondata.ITEM_WEIGHT = "";
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.Viscosity))
            {
                jsondata.item_name = "粘度";
                jsondata.item_value = jsondata.Viscosity;
                jsondata.ITEM_WEIGHT = "";
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.Fineness))
            {
                jsondata.item_name = "细度";
                jsondata.item_value = jsondata.Fineness;
                jsondata.ITEM_WEIGHT = "";
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.Filter))
            {
                jsondata.item_name = "过筛况";
                jsondata.item_value = jsondata.Filter;
                jsondata.ITEM_WEIGHT = "";
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (!string.IsNullOrEmpty(jsondata.chutie))
            {
                jsondata.item_name = "除铁要求";
                jsondata.item_value = jsondata.chutie;
                jsondata.ITEM_WEIGHT = "";
                await _dal.AddTestValue(intStep, jsondata);
                bl = true;
            }
            if (bl)
            {
              await  _dal.UpdateShipCartInfo(jsondata);
                message.success = true;
                message.msg = "添加成功";
            }
            return message;
        }
    }
}
