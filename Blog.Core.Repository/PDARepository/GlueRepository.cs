using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.Repository.Base;

namespace Blog.Core.Repository.PDARepository
{
    public class GlueRepository : BaseRepository<GlueModel>, IGlueRepository
    {
        public GlueRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// 增加溶液值
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddTestValue(string itemName, GlueSubmitParam model)
        {
            string strAdd = string.Format(@"insert into t_mes_stir_info
  (shipment_no,
   item_name,
   item_value,
   date_time,
   username,
step_name)
values
  ('{0}', '{1}', '{2}', sysdate, '{3}', '{4}')
", model.shipCard, itemName, model.solutions_Name, model.glueUser, model.productType);
           return  await base.ExecuteCommand(strAdd,new { }) >0;
        }
        /// <summary>
        /// 获取出货牌信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<GlueModel> GetSerialnumber(GetShipCardParam jsondata)
        {
            return (await base.SqlQuerySingleAsync<GlueModel>(@"select  t.s_number,t.s_date,to_char(sysdate,'yyyy/mm/dd') sys_Date from t_mes_serialnumber t where t.s_itemname=@itemname", new { itemname = jsondata.productType }));
        }

        /// <summary>
        /// 打胶页面加载设备信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<List<GlueModel>> GlueLoad(GlueLoadParam jsondata)
        {
            var result = await base.SqlQuery<GlueModel>(@"select h.device_sn, h.device_id, s1.work_stationname
from   t_smo_workstationdevice w,
       t_co_device             h,
       t_co_device             d1,
       t_smo_workstationdevice w1,
       t_smo_workstation       s1
where  w.device_id = h.device_id
and    w.work_stationid = s1.work_stationid
and    d1.device_id = w1.device_id
and    w1.work_stationid = s1.work_stationid
and    d1.device_sn = @device_sn", new { device_sn = jsondata.device_sn });
           return  result;
        }
        /// <summary>
        /// 插入项目名称信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<bool> InsertSerialnumber(GetShipCardParam jsondata)
        {
            string strAdd = string.Format(@"insert into t_mes_serialnumber(s_itemname,s_type,s_number,s_date)values('{0}','{1}','{2}','{3}')", jsondata.productType, "PACK", "01", "20200101");
            return  await base.ExecuteCommand(strAdd, new { })>0;
        }

        /// <summary>
        /// 增加出货牌
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<bool> InsertShipcart(GlueSubmitParam jsondata)
        {
            string strAdd = string.Format(@"insert into T_MES_MACHINE_USEINFO
  (shipment_no,
   input_sum,
   device_no,
   add_date,
USER_NAME)
values
  ('{0}', '{1}', '{2}',sysdate, '{3}')", jsondata.shipCard, jsondata.solution_SUM, jsondata.device_sn, jsondata.glueUser);
            return await base.ExecuteCommand(strAdd, new { })>0;
            
        }
        /// <summary>
        /// 更新出货牌流水号
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShipcartCode(GlueSubmitParam jsondata)
        {
            // 更新出货牌流水号
            string strUpdate = string.Format(@"update t_mes_serialnumber t set t.s_number='{0}',t.s_date='{1}' where  t.s_itemname='{2}'", jsondata.strLSH, jsondata.sys_Date, jsondata.productType);
            return  await base.ExecuteCommand(strUpdate, new { })>0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTyep"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> wip_QTY( GlueSubmitParam model)
        {
            //添加
            string strAddUpdate = "";
                strAddUpdate = string.Format(@"insert into T_MES_WIP_QTY
  (STATION_NAME, MO_NUMBER, SHIPT_NO,QTY_INPUT, START_DATE)
values
  ('{0}', '{1}', '{2}', '{3}',sysdate) ", model.device_sn, model.moNumber, model.shipCard,model.solution_SUM);
           return await base.ExecuteCommand(strAddUpdate, new { })>0;
        }

        public async Task<bool> wip_QTY(GlueCloseParam model)
        {
            //添加
            string strAddUpdate = "";
                strAddUpdate = string.Format("update T_MES_WIP_QTY w set w.qty_out='{0}' where w.mo_number='{1}' and w.station_name='{2}'and w.shipt_no='{3}' and w.statue=0", model.outQTY, model.moNumber, model.device_sn, model.shipCard);
            return await base.ExecuteCommand(strAddUpdate, new { }) > 0;
        }
        /// <summary>
        /// 更新出货牌状态
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShipCartState(GlueCloseParam jsondata)
        {
            string strUpdate = string.Format(@"update T_MES_MACHINE_USEINFO t set t.print=0,t.statue=1,t.out_qty='{1}',t.CURRENT_STEPS='{2}',t.NEXT_STEP='{3}',t.UPDATE_DATE=sysdate where t.shipment_no='{0}' and t.statue=0", jsondata.shipCard, jsondata.outQTY, jsondata.productType, jsondata.productItem);
            return await base.ExecuteCommand(strUpdate) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsondata">设备编号</param>
        /// <returns></returns>
        public async Task<List<GlueModel>> GetGlueModel(GlueLoadParam jsondata)
        {
            string strLookMH = string.Format(@"select t.shipment_no shipCard, t.input_sum, t.out_qty outQTY, t.USER_NAME, s.item_value
from   T_MES_MACHINE_USEINFO t, t_mes_stir_info s
where  t.device_no = '{0}'
and    t.statue = 0
and    s.shipment_no = t.shipment_no
and    s.item_name = '溶液名称'", jsondata.device_sn);
          return  await base.SqlQuery<GlueModel>(strLookMH);
        }
        /// <summary>
        ///  打胶参数录入-页面加载
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<GlueDetailModel> GetCurrentStep(GlueDetailParam jsondata)
        {
            string strLook = string.Format(@"select  t.current_steps,t.next_step from T_MES_MACHINE_USEINFO t where t.shipment_no='{0}'", jsondata.shipCard);
           return  await base.SqlQuerySingleAsync<GlueDetailModel>(strLook,new { }) ;
        }
        /// <summary>
        /// 出货牌名称获取
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<List<GlueDetailModel>> StrstepNameSelecte(CurrenStepSelectChangesParam jsondata)
        {
            string strstepNameSelecte = string.Format(@"select d.item_name, d.item_value,d.MONITOR,d.USERNAME,d.ITEM_WEIGHT
from   t_mes_stir_info d, T_MES_MACHINE_USEINFO c
where  d.shipment_no = c.shipment_no
and    d.step_num = c.step_num
and    d.step_name='{0}'
and    c.shipment_no = '{1}'", jsondata.stepName, jsondata.shipCard);
          return  await base.SqlQuery<GlueDetailModel>(strstepNameSelecte, new { });
        }
        /// <summary>
        /// 获取出货牌当前步骤
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<GlueDetailModel> GetShipCart(GlueDetailModel jsondata)
        {
            string strStep = string.Format(@"select b.step_num from T_MES_MACHINE_USEINFO b where b.shipment_no='{0}'and b.device_no='{1}'", jsondata.shipCard, jsondata.MachineNo);
            return  await base.SqlQuerySingleAsync<GlueDetailModel>(strStep, new { });
        }
        /// <summary>
        /// 步骤参数录入
        /// </summary>
        /// <param name="intStep">当前工步数</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddTestValue(int intStep, GlueDetailModel model)
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
", model.shipCard, model.stepName, model.item_name, model.item_value, model.USERNAME, model.MONITOR, intStep, model.ITEM_WEIGHT);
          return  await base.ExecuteCommand(strAdd, new { })>0;
        }

      
        /// <summary>
        /// 更新出货牌信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShipCartInfo(GlueDetailModel model)
        {
            string strUpdate = string.Format(@"update T_MES_MACHINE_USEINFO a set a.current_steps='{0}',a.next_step='{1}',a.step_num=a.step_num+1,a.update_date=sysdate where a.shipment_no='{2}'and a.device_no='{3}'", model.current_steps, model.next_step, model.shipCard, model.MachineNo);
           return  await base.ExecuteCommand(strUpdate,new { })>0;
        }
    }
}
