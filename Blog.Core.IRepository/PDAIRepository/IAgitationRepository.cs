using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IRepository.Base;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;

namespace Blog.Core.IRepository.PDAIRepository
{
    public interface IAgitationRepository : IBaseRepository<t_gule_step_info>
    {
        #region 设备编号文本框 文本改变监听事件
        Task<AgitationModel> GetLookMH(DeviceSelectParam model);
        #endregion




        #region 组别文本回车事件
        /// <summary>
        /// 查询package计划
        /// </summary>
        /// <returns></returns>
        Task<List<AgitationModel>> GetLook(GroupKeyPressParam model);
        #endregion
        /// <summary>
        /// 获取出货牌信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<AgitationModel> GetSerialnumber(GetShipCardParam model);
        /// <summary>
        /// 更新出货牌流水号
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<bool> UpdateShipcartCode(AgitationModel model);
        /// <summary>
        /// 搅拌提交信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> InsertSubmit(AgitationSubmitParam model);
        /// <summary>
        /// 记录组别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> InserGroup(AgitationSubmitParam model);
        /// <summary>
        /// 更新t_CO_package状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdatePackageState(AgitationSubmitParam model);
        /// <summary>
        /// 更新出货牌状态
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<bool> UpdateShipCartState(AgitationModel model);
        /// <summary>
        /// 获取出货牌信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AgitationModel> GetShipCartInfo(AgitationUpdateShipCartParam model);
        /// <summary>
        /// 更新出货牌固含量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateShipCartBySolid_content(AgitationUpdateShipCartParam model);
        /// <summary>
        /// 新增出牌参数信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> InsertShipCartinfo(AgitationUpdateShipCartParam model);
        /// <summary>
        /// 页面加载设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<AgitationModel>> GlueLoad(AgitationLoad model);
        Task<bool> wip_QTY(CloseParam model);
        Task<bool> wip_QTY(AgitationSubmitParam model);
        //搅拌详细页
        Task<List<AgitationDetailModel>> StrstepNameSelecte(AgitationStrstepNameSelecteParam model);
        Task<AgitationGetItemInfo> GetTimeCheck(AgitationItems model);
        Task<AgitationDetailModel> GetStepNumber(AgitationDetailParam model);

        Task<bool> AddTestValue(int intStep, AgitationDetailModel model);
        Task<bool> UpdateShipCartInfo(AgitationDetailModel model);


        Task<List<t_gule_step_infoVM>> GetStepList(t_gule_step_infoVM model);
        Task<bool> CheckMartter(t_gule_step_infoVM model);
        Task<bool> DeleteStepInfo(AgitationDetailParam model, AgitationStrstepNameSelecteParam stepModel);
        Task<t_gule_step_infoVM> GetStepListAllByStepName(t_gule_step_infoVM model);
    }
}
