using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.Repository.Base;

namespace Blog.Core.Repository.PDARepository
{
    public class AgitationRepository : BaseRepository<t_gule_step_info>, IAgitationRepository
    {
        public AgitationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #region 首页
        /// <summary>
        /// 查询package计划
        /// </summary>
        /// <param name="model">moNumber,groupName,strType</param>
        /// <returns></returns>
        public async Task<List<AgitationModel>> GetLook(GroupKeyPressParam model)
        {
            string strLook = string.Format(@"select pa.package_id,
       pa.packagenumber,
       pa.group_no,
       pa.cell_model,
       pa.Plan_Fe_Number,
	   pa.engineer
from   t_co_package pa
where  pa.packagenumber = '{0}'
and pa.group_no='{1}'
and pa.polarity='{2}'", model.moNumber,model.groupName, model.productType.Substring(0, 2));
            return await base.SqlQuery<AgitationModel>(strLook,new { });
        }

        /// <summary>
        ///  设备文本改变监听事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AgitationModel> GetLookMH(DeviceSelectParam model)
        {
            string strLookMH = string.Format(@"select t.package_id,t.technology,t.engineer,t.group_name,t.shipment_no,t.formula_no,t.solid_content,t.requirement,t.input_sum ,t.stir_sum,t.out_qty,t.USER_NAME,t.TYPE_NAME from T_MES_MACHINE_USEINFO t where t.device_no='{0}'  and t.statue=0", model.device_sn);
            return  await base.SqlQuerySingleAsync<AgitationModel>(strLookMH, new { });
        }

        /// <summary>
        /// 获取出货牌信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<AgitationModel> GetSerialnumber(GetShipCardParam model)
        {
            return (await base.SqlQuerySingleAsync<AgitationModel>(@"select  t.s_number,t.s_date,to_char(sysdate,'yyyy/mm/dd') sys_Date from t_mes_serialnumber t where t.s_itemname=@itemname", new { itemname = model.productType }));
        }
        /// <summary>
        /// 获取出货牌信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AgitationModel> GetShipCartInfo(AgitationUpdateShipCartParam model)
        {
            string strQTY = string.Format(@"select 1 from T_MES_MACHINE_USEINFO c where c.shipment_no='{0}' ", model.shipment_no);
            return await base.SqlQuerySingleAsync<AgitationModel>(strQTY,new { });
        }
        /// <summary>
        /// 页面加载设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<AgitationModel>> GlueLoad(AgitationLoad model)
        {
            var result = await base.SqlQuery<AgitationModel>(@"select h.device_sn, h.device_id, s1.work_stationname
from   t_smo_workstationdevice w,
       t_co_device             h,
       t_co_device             d1,
       t_smo_workstationdevice w1,
       t_smo_workstation       s1
where  w.device_id = h.device_id
and    w.work_stationid = s1.work_stationid
and    d1.device_id = w1.device_id
and    w1.work_stationid = s1.work_stationid
and    d1.device_sn = @device_sn", new { device_sn = model.device_sn });
            return result;
        }

        /// <summary>
        /// 记录组别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InserGroup(AgitationSubmitParam model)
        {
            string strAdd = string.Format(@" insert into t_mes_groupname_info
   (group_name_id, group_name, barcode, mo, processes,create_user)
 values
   (SEQ_GROUPNAME_ID.nextval, '{0}', '{1}', '{2}', '{3}', '{4}')
", model.groupName, model.shipment_no, model.moNumber, model.productType, model.USER_NAME);
            return await base.ExecuteCommand(strAdd, new { })>0;
        }
        /// <summary>
        /// 新增出牌参数信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertShipCartinfo(AgitationUpdateShipCartParam model)
        {
            string strUpdate = string.Format(@"insert into t_mes_stir_info (SHIPMENT_NO, STEP_NAME, ITEM_NAME, ITEM_VALUE, DATE_TIME, USERNAME )
values ('{1}', '粘度-更新', '粘度', '{0}', sysdate, '{2}')", model.viscosity,model.shipment_no, model.USER_NAME);
            return await  base.ExecuteCommand(strUpdate,new { })>0;
        }

        /// <summary>
        /// 搅拌提交信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertSubmit(AgitationSubmitParam model)
        {
            string strAdd = string.Format(@"insert into T_MES_MACHINE_USEINFO
  (technology,
   group_name,
   shipment_no,
   formula_no,
   requirement,
   input_sum,
   stir_sum,
   device_no,
   add_date,
TYPE_NAME,
USER_NAME,
Engineer,current_steps,package_id)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',sysdate,'{8}','{9}','{10}','{11}','{12}')", model.moNumber, model.groupName, model.shipment_no, model.Formula, model.requirement, model.input_sum, model.stir_sum, model.device_sn,model.A_Type, model.USER_NAME, model.engineer, model.productType, model.packagenumber);
           return  await base.ExecuteCommand(strAdd,new { })>0;
        }
        /// <summary>
        /// 更新t_CO_package状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePackageState(AgitationSubmitParam model)
        {
            string strAdd = string.Format(@" update t_co_package m
set    m.stutas = '10', m.shift_anode = nvl(m.shift_anode, 0) + 1
where  m.package_id = '{0}'
and    (m.stutas < 12 or m.stutas = '10')
", model.packagenumber);
            return await base.ExecuteCommand(strAdd, new { })>0; 
        }
        /// <summary>
        /// 更新出货牌固含量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShipCartBySolid_content(AgitationUpdateShipCartParam model)
        {
            string strUpdate = string.Format(@"update T_MES_MACHINE_USEINFO t set t.solid_content='{0}',t.update_date=sysdate where  t.shipment_no='{1}'", model.solid_content, model.shipment_no);
            return await base.ExecuteCommand(strUpdate,new { })>0;
        }

        /// <summary>
        /// 更新出货牌流水号
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShipcartCode(AgitationModel model)
        {
            // 更新出货牌流水号
            string strUpdate = string.Format(@"update t_mes_serialnumber t set t.s_number='{0}',t.s_date='{1}' where  t.s_itemname='{2}'", model.strLSH, model.sys_Date, model.productType);
            return await base.ExecuteCommand(strUpdate, new { }) > 0;
        }
        /// <summary>
        /// 更新出货牌状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShipCartState(AgitationModel model)
        {
            string strUpdate = string.Format(@"update T_MES_MACHINE_USEINFO t set t.print=0,t.statue=1,t.out_qty='{1}',t.CURRENT_STEPS='{2}',t.NEXT_STEP='{3}',t.UPDATE_DATE=sysdate where t.shipment_no='{0}' and t.statue=0",model.shipment_no,model.out_qty, model.productType, model.productItem);
            return await base.ExecuteCommand(strUpdate) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTyep"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> wip_QTY(CloseParam model)
        {
            //添加
            string strAddUpdate = "";
                strAddUpdate = string.Format("update T_MES_WIP_QTY w set w.qty_out='{0}' where w.mo_number='{1}' and w.station_name='{2}'and w.shipt_no='{3}' and w.statue=0", model.out_qty, model.moNumber, model.device_sn, model.shipment_no);
            return await base.ExecuteCommand(strAddUpdate, new { }) > 0;
        }
        public async Task<bool> wip_QTY(AgitationSubmitParam model)
        {
            //添加
            string strAddUpdate = "";
                strAddUpdate = string.Format(@"insert into T_MES_WIP_QTY
  (STATION_NAME, MO_NUMBER, SHIPT_NO,QTY_INPUT, START_DATE)
values
  ('{0}', '{1}', '{2}', '{3}',sysdate) ", model.device_sn, model.moNumber, model.shipment_no, model.input_sum);
            
            return await base.ExecuteCommand(strAddUpdate, new { }) > 0;
        }
        #endregion
        #region 搅拌详细页
        /// <summary>
        /// 根据出货牌查当前步骤的工序数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<AgitationDetailModel>> StrstepNameSelecte(AgitationStrstepNameSelecteParam model)
        {
            //and d.step_num = c.step_num
            string strstepNameSelecte = string.Format(@"select d.item_name, d.item_value,d.MONITOR,d.USERNAME,d.ITEM_WEIGHT,c.current_steps,c.next_step
from   t_mes_stir_info d, T_MES_MACHINE_USEINFO c
where  d.shipment_no = c.shipment_no
and    d.step_name='{0}'
and    c.shipment_no = '{1}'", model.stepName, model.shipCard);
            return await base.SqlQuery<AgitationDetailModel>(strstepNameSelecte, new { });
        }
        /// <summary>
        /// 检查打胶的CMC/PVDF胶液的有效期
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AgitationGetItemInfo> GetTimeCheck(AgitationItems model)
        {
            string strLook = string.Format(@"SELECT t.out_qty, t.TECHNOLOGY, t.type_name , instr(UPPER(s.item_value), 'CMC') AS CMC , instr(UPPER(s.item_value), 'PVDF') AS PVDF , to_char(t.coatingdate, 'yyyy-mm-dd hh24:mi:ss') AS coatingdate , ROUND(TO_NUMBER(sysdate - t.effectivedate) * 24 * 60) AS  timeDifference FROM T_MES_MACHINE_USEINFO t, T_MES_STIR_INFO S WHERE (s.shipment_no = t.shipment_no AND s.item_name = '溶液名称' AND t.shipment_no = '{0}')", model.item_No);
            return await base.SqlQuerySingleAsync<AgitationGetItemInfo>(strLook, new { });

        }
        /// <summary>
        /// 步骤参数录入
        /// </summary>
        /// <param name="intStep">当前工步数</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddTestValue(int intStep, AgitationDetailModel model)
        {
            string strAdd = string.Format(@"insert into t_mes_stir_info
  (shipment_no,
   step_name,
   item_name,
   item_value,
   date_time,
   username,
   monitor,
   step_num,
ITEM_WEIGHT)
values
  ('{0}', '{1}', '{2}', '{3}', sysdate, '{4}', '{5}','{6}','{7}')
", model.shipCard, model.stepName, model.item_name, model.item_value, model.USERNAME, model.MONITOR, intStep, model.item_number);
            return await base.ExecuteCommand(strAdd, new { }) > 0;
        }
        /// <summary>
        /// 更新出货牌信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShipCartInfo(AgitationDetailModel model)
        {
            string strUpdate = string.Format(@"update T_MES_MACHINE_USEINFO a set a.current_steps='{0}',a.next_step='{1}',a.step_num=a.step_num+1,a.update_date=sysdate where a.shipment_no='{2}'and a.device_no='{3}'", model.groupName, model.next_step, model.shipCard, model.device_no);
            return await base.ExecuteCommand(strUpdate) > 0;
        }
        public async Task<AgitationDetailModel> GetStepNumber(AgitationDetailParam model)
        {
            string strStep = string.Format(@"select b.step_num from T_MES_MACHINE_USEINFO b where b.shipment_no='{0}'and b.device_no='{1}'", model.shipCard, model.device_no);
          return    await base.SqlQuerySingleAsync<AgitationDetailModel>(strStep, new { });
        }
        /// <summary>
        /// 获取工步名称列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<t_gule_step_infoVM>> GetStepList(t_gule_step_infoVM model)
        {
            string strStep = string.Format(@"select distinct t.step_number,t.step_name from t_gule_step_info t ,t_co_package c
where t.model_code=c.cell_model
and t.polarity=C.POLARITY
and t.polarity='{0}'
and c.packagenumber='{1}'
order by t.step_number", model.POLARITY, model.package);
            return await base.SqlQuery<t_gule_step_infoVM>(strStep, new { });

        }
        public async Task<List<t_gule_step_infoVM>> GetStepListAll(t_gule_step_infoVM model)
        {
            string strStep = string.Format(@"select * from t_gule_step_info t ,t_co_package c
where t.model_code=c.cell_model
and t.polarity=C.POLARITY
and t.polarity='{0}'
and c.packagenumber='{1}'
and t.STEP_NAME='{2}'
order by t.step_number", model.POLARITY, model.package,model.STEP_NAME);
            return await base.SqlQuery<t_gule_step_infoVM>(strStep, new { });

        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<t_gule_step_infoVM> GetStepListAllByStepName(t_gule_step_infoVM model)
        {
            string strStep = string.Format(@"select * from t_gule_step_info t ,t_co_package c
where t.model_code=c.cell_model
and t.polarity=C.POLARITY
and t.polarity='{0}'
and c.packagenumber='{1}'
and t.STEP_NAME='{2}'
order by t.step_number", model.POLARITY, model.package,model.STEP_NAME);
            return await base.SqlQuerySingleAsync<t_gule_step_infoVM>(strStep, new { });

        }
        /// <summary>
        ///校验物料信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> CheckMartter(t_gule_step_infoVM model)
        {
            var resule = (await this.GetStepListAll(model)).Where(m => m.ITEM_CODE == model.ITEM_CODE).FirstOrDefault();
            if (resule != null)
            {
                //&&m.POWDER_UPPER<=model.POWDER_Value&& model.POWDER_Value >= m.POWDER_LOWER
                if (resule.POWDER_UPPER >= model.POWDER_Value && model.POWDER_Value >= resule.POWDER_LOWER)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        ///删除工步信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> DeleteStepInfo(AgitationDetailParam model, AgitationStrstepNameSelecteParam stepModel)
        {
            string strStep = string.Format(@"delete  t_mes_stir_info t where t.SHIPMENT_NO='{0}' and t.STEP_NAME='{1}'", model.shipCard, stepModel.stepName);
            return await base.ExecuteCommand(strStep) > 0;
        }
        #endregion

    }
}
