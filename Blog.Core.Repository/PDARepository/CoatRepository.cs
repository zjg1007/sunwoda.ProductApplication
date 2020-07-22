using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.Repository.Base;

namespace Blog.Core.Repository.PDARepository
{
    public class CoatRepository : BaseRepository<CoatModel>, ICoatRepository
    {
        public CoatRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// 设备基础信息DeviceChanged
        /// </summary>
        /// <returns></returns>
        public async Task<CoatModel> DeviceChanged(DeviceChangedParam model)
        {
            string strLook = string.Format(@"select t.shipment_no,
       t.previous_shipment_no,
       t.technology,
       t.group_name,
       t.input_sum,
       铝箔批号,
       浆料批号,
    分条数 Partition_Sum,
       X或Y Partition_XY,
卷料批号 ItemName,
       t.engineer,
	   h.monitor,
       t.user_name
from   T_MES_MACHINE_USEINFO t,
       (select f.shipment_no,f.monitor,
               max(decode(f.item_name, '铝箔批号', f.item_value, null)) 铝箔批号,
               max(decode(f.item_name, '浆料批号', f.item_value, null)) 浆料批号,
max(decode(f.item_name, '卷料批号', f.item_value, null)) 卷料批号,
max(decode(f.item_name, '分条数', f.item_value, null)) 分条数,
               max(decode(f.item_name, 'X或Y', f.item_value, null)) X或Y
        from   t_mes_stir_info f
        where  f.shipment_no in (select t.shipment_no
                                from   T_MES_MACHINE_USEINFO t
                                where  t.device_no = '{0}'
                                and    t.statue = 0)
        group  by f.shipment_no,f.monitor) h
where  t.shipment_no = h.shipment_no(+)
and    t.device_no = '{0}'
and    t.statue = 0", model.device_no);
            return await base.SqlQuerySingleAsync<CoatModel>(strLook, new { });
        }
        /// <summary>
        /// 计算时差判断浆料是否过期
        /// </summary>
        /// <returns></returns>
        public async Task<CoatModel> MeterialOverdue(CoatModel model)
        {
            string strLook = string.Format(@"select t.package_id,t.engineer,t.out_qty,t.TECHNOLOGY,t.type_name,to_char(t.coatingdate,'yyyy-mm-dd hh24:mi:ss') coatingdate,ROUND(TO_NUMBER(sysdate - t.effectivedate) * 24 * 60)  timeDifference  from T_MES_MACHINE_USEINFO t where t.shipment_no='{0}'", model.previous_shipment_no);
            return await base.SqlQuerySingleAsync<CoatModel>(strLook, new { });
        }
        /// <summary>
        /// 出货牌数量
        /// </summary>
        /// <returns></returns>
        public async Task<CoatModel> ShipCartNumber(CoatModel model)
        {
            string strQTY = string.Format(@"select sum(t.input_sum) qty from T_MES_MACHINE_USEINFO t where t.previous_shipment_no='{0}' ", model.previous_shipment_no);
            return await base.SqlQuerySingleAsync<CoatModel>(strQTY, new { });
        }
        /// <summary>
        /// 未关结出货牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<CoatModel>> ShipCartNoClose(CoatModel model)
        {
            string strLookMac = string.Format(@"select t.shipment_no
                                  from T_MES_MACHINE_USEINFO t
                                 where t.device_no = '{0}'
                                   and t.statue = 0", model.device_no);
            return await base.SqlQuery<CoatModel>(strLookMac, new { });
        }
        /// <summary>
        /// 查流水号（用于生成出货牌）
        /// </summary>
        /// <returns></returns>
        public async Task<CoatModel> GetShipCartNo(CoatModel model)
        {
             string strLook = string.Format(@"select  t.s_number,t.s_date,to_char(sysdate,'yyyy/mm/dd') sys_Date from t_mes_serialnumber t where t.s_itemname='{0}'",model. productType);
          
            return await base.SqlQuerySingleAsync<CoatModel>(strLook, new { });
        }
        /// <summary>
        /// 更新流水号（用于生成出货牌）
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateShipCartNo(CoatModel model)
        {
            string strUpdate = string.Format(@"update t_mes_serialnumber t set t.s_number='{0}',t.s_date='{1}' where  t.s_itemname='{2}'", model. strLSH, model.sys_Date, model.productType);

            return await base.ExecuteCommand(strUpdate, new { }) > 0;
        }
   /// <summary>
        /// 新增流水号（用于生成出货牌）
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddShipCartNo(CoatModel model)
        {
            string strAdd = string.Format(@"insert into t_mes_serialnumber(s_itemname,s_type,s_number,s_date)values('{0}','{1}','{2}','{3}')", model.productType, "PACK", "01", "20200311");

            return await base.ExecuteCommand(strAdd, new { }) > 0;
        }

  /// <summary>
        /// 物料是否存在BOM
        /// </summary>
        /// <returns></returns>
        public async Task<List<CoatModel>> MeterialByBom(CoatModel model)
        {
            string strLook = string.Format(@"select d.cbd_bomid
from   t_co_bom b, t_co_bom_detail d
where  d.cb_bomid = b.cb_bomid
and    b.cb_item_code = '{0}'
and    d.cbd_item_code = '{1}'
union
select s.cbds_id
from   t_co_bom b, t_co_bom_detail d, t_co_bom_detail_sub s
where  d.cb_bomid = b.cb_bomid
and    d.cbd_bomid = s.cbd_bomid
and    b.cb_item_code = '{0}'
and    s.cbds_item_code = '{1}'", model.technology, model.StrMaterialNO);
            return await base.SqlQuery<CoatModel>(strLook, new { }) ;
        }
        /// <summary>
        /// 记录组别
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddGroupName(CoatModel model)
        {
            string strAdd = string.Format(@" insert into t_mes_groupname_info
   (group_name_id, group_name, barcode, mo, processes,create_user)
 values
   (SEQ_GROUPNAME_ID.nextval, '{0}', '{1}', '{2}', '{3}', '{4}')
", model.group_name, model.shipment_no, model.technology, model.productType,model.user_name);
            return await base.ExecuteCommand(strAdd, new { }) > 0;
        } 
        /// <summary>
        /// 出货批号
        /// </summary>
        /// <returns></returns>
        public async Task<List<CoatModel>> GetShipcart(CoatModel model)
        {
            string strLook = string.Format(@"select *from T_MES_MACHINE_USEINFO t where t.shipment_no='{0}'", model.shipment_no);
            return await base.SqlQuery<CoatModel>(strLook, new { }) ;
        } 
        /// <summary>
        /// 增加出货牌信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddShipcart(CoatModel model)
        {
            string addMachine = string.Format(@"insert into T_MES_MACHINE_USEINFO
  ( SHIPMENT_NO, DEVICE_NO, TECHNOLOGY, GROUP_NAME,INPUT_SUM,PREVIOUS_SHIPMENT_NO,STATUE,user_name, ENGINEER,CURRENT_STEPS,NEXT_STEP,ADD_DATE,type_name,package_id)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','0','{6}','{7}','{8}','{9}',sysdate,'{10}','{11}')", model.shipment_no, model.device_no, model.technology, model.group_name, model.input_sum, model.previous_shipment_no, model.user_name, model.engineer, model.productType, model.strItem, model.type_name, model.package_id);
            return await base.ExecuteCommand(addMachine, new { }) > 0;
        }
/// <summary>
        /// 删除出货牌信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeteleShipcart(CoatModel model)
        {
            string strDelete = string.Format(@" delete t_mes_machine_useinfo f where f.statue=0 and f.shipment_no='{0}'
", model.shipment_no);
            return await base.ExecuteCommand(strDelete, new { }) > 0;
        }
        /// <summary>
        ///更新t_CO_package状态
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpShipcart(CoatModel model)
        {
            string strUpdate = string.Format(@" update t_CO_package m
set    m.stutas = '12'
where  m.package_id = '{0}'
and    m.stutas < 12
", model.package_id);
            return await base.ExecuteCommand(strUpdate, new { }) > 0;
        }
  /// <summary>
        ///添加物料
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddMetrial(CoatModel model)
        {
            string strAdd = string.Format(@"insert into t_mes_stir_info
  (shipment_no,
   item_name,
   item_value,
   date_time,
   username,
   monitor,
step_name)
values
  ('{0}', '{1}', '{2}', sysdate, '{3}', '{4}','{5}')
", model.shipment_no, model.itemName, model.itemValue, model.user_name, model.monitor, model.productType);
            return await base.ExecuteCommand(strAdd, new { }) > 0;
        }
         /// <summary>
        ///关结成功
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Close(CoatModel model)
        {
            string strUpdate = string.Format(@"update T_MES_MACHINE_USEINFO t set t.statue=1,t.out_qty='{0}',t.UPDATE_DATE=sysdate where t.shipment_no='{1}' and t.statue=0", model.out_qty, model.shipment_no);
            return await base.ExecuteCommand(strUpdate, new { }) > 0;
        }
        /// <summary>
        ///更新投入数量
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CoatUpdate(CoatModel model)
        {
            string strUpdate = string.Format(@"update T_MES_MACHINE_USEINFO  set input_sum=input_sum+'{0}' where  shipment_no='{1}' and statue=0",model.input_sum, model.shipment_no);
            return await base.ExecuteCommand(strUpdate, new { }) > 0;
        }
        public async Task<bool> CheckUserRole(CheckUserRoleparam model)
        {
            string strInsert = string.Format(@"insert into bss_sys_log (USERNAME, TRUENAME, CLIENTIP, OPERTYPE, MODULENAME, OPERCONTENT)
values ('{0}', '{1}','PDA程式', '权限验证', '{2}', '{3}')
", model.user_name ,model.username, model.productType, "跨工单投入权限验证|" + model.previous_shipment_no + "|" + model.technology + "|" + model.device_no);
            return await base.ExecuteCommand(strInsert, new { }) > 0;
        }
    }
}
