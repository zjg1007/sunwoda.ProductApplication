using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IRepository.Base;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;

namespace Blog.Core.IRepository.PDAIRepository
{
    public  interface IGlueRepository : IBaseRepository<GlueModel>
    {
        Task<bool>  wip_QTY( GlueSubmitParam model);
        Task<bool> wip_QTY( GlueCloseParam model);
        /// <summary>
        /// 增加溶液值
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddTestValue(string itemName, GlueSubmitParam model);
        /// <summary>
        /// 打胶页面加载设备信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<List<GlueModel>> GlueLoad(GlueLoadParam jsondata);
        /// <summary>
        /// 更新出货牌流水号
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<bool> UpdateShipcartCode(GlueSubmitParam jsondata);
        /// <summary>
        /// 增加出货牌
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<bool> InsertShipcart(GlueSubmitParam jsondata);
        /// <summary>
        /// 获取出货牌信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<GlueModel> GetSerialnumber(GetShipCardParam jsondata);
        /// <summary>
        /// 插入项目名称信息
        /// </summary>
        /// <returns></returns>
        Task<bool> InsertSerialnumber(GetShipCardParam jsondata);

        /// <summary>
        /// 更新出货牌状态
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<bool> UpdateShipCartState(GlueCloseParam jsondata);
        Task<List<GlueModel>> GetGlueModel(GlueLoadParam jsondata);

        /// <summary>
        ///  打胶参数录入-页面加载
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<GlueDetailModel> GetCurrentStep(GlueDetailParam jsondata);

        /// <summary>
        /// 出货牌名称获取
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<List<GlueDetailModel>> StrstepNameSelecte(CurrenStepSelectChangesParam jsondata);
        /// <summary>
        ///获取出货牌当前步骤
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<GlueDetailModel> GetShipCart(GlueDetailModel jsondata);
        /// <summary>
        /// 步骤参数录入
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddTestValue(int intStep, GlueDetailModel model);
        /// <summary>
        /// 更新出货牌信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateShipCartInfo(GlueDetailModel model);
    }
}
