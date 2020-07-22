using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IServices.BASE;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;

namespace Blog.Core.IServices.PDAIServices
{
    public interface IGlueServices : IBaseServices<GlueModel>
    {
        /// <summary>
        /// 打胶加载页面获取对应设备信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<MessageModel<List<GlueLoadReturns>>> GlueLoad(GlueLoadParam jsondata);
        /// <summary>
        /// 获取出货牌
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<MessageModel<GetShipCardReturns>> GetShipCard(GetShipCardParam jsondata);
        Task<MessageModel<GlueSubmitRaturns>> GlueSubmit(GlueSubmitParam jsondata);
        Task<MessageModel<string>> GlueClose(GlueCloseParam jsondata);
        Task<List<DeviceSelectChangesReturns>> DeviceSelectChanges(GlueLoadParam jsondata);
        /// <summary>
        /// 打胶参数录入-页面加载
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>

        Task<GlueDetailModel> GetCurrentStepLoad(GlueDetailParam jsondata);
        /// <summary>
        /// 打胶参数录入-当前步骤值改变事件
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<CurrenStepSelectChangesReturns> CurrenStepSelectChanges(CurrenStepSelectChangesParam jsondata);
        /// <summary>
        /// 打胶参数录入-提交
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        Task<MessageModel<string>> SubmitGlue(GlueDetailModel jsondata);
    }
}
