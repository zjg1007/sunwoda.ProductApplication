using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Common;
using Blog.Core.IServices.PDAIServices;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.SwaggerHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Blog.Core.SwaggerHelper.CustomApiVersion;

namespace Blog.Core.Controllers.PDA.V2
{
    /// <summary>
    /// 搅拌
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AgitationController : ControllerBase
    {
        readonly IAgitationServices _agitationServices;
        readonly IRedisCacheManager _redisCacheManager;
        public AgitationController(IAgitationServices agitationServices, IRedisCacheManager redisCacheManager)
        {
            _agitationServices = agitationServices;
            _redisCacheManager = redisCacheManager;
        }
        #region 搅拌首页
        /// <summary>
        /// 页面加载-设备列表
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "Load")]
        public async Task<MessageModel<List<AgitationModel>>> Load(AgitationLoad jsondata)
        {
            return await _agitationServices.Load(jsondata);
        }
        /// <summary>
        /// 设备基础信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "DeviceSelect")]
        public async Task<DeviceSelectReturns> DeviceSelect(DeviceSelectParam jsondata)
        {
            return await _agitationServices.DeviceSelect(jsondata);
        }
        /// <summary>
        /// 组别校验
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GroupKeyPress")]
        public async Task<MessageModel<List<GroupKeyPressReturns>>> GroupKeyPress(GroupKeyPressParam jsondata)
        {
            return await _agitationServices.GroupKeyPress(jsondata);
        }
        /// <summary>
        /// 获取出货牌
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GetShipCarts")]
        public async Task<MessageModel<GetShipCardReturns>> GetShipCarts(GetShipCardParam jsondata)
        {
            return await _agitationServices.GetShipCarts(jsondata);
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "Submit")]
        public async Task<MessageModel<AgitationSubmitParam>> Submit(AgitationSubmitParam jsondata)
        {
            return await _agitationServices.Submit(jsondata);
        }
        /// <summary>
        /// 关结
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "Close")]
        public async Task<MessageModel<string>> Close(CloseParam jsondata)
        {
            return await _agitationServices.Close(jsondata);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "UpdateShipCart")]
        public async Task<MessageModel<AgitationUpdateShipCartParam>> UpdateShipCart(AgitationUpdateShipCartParam jsondata)
        {
            return await _agitationServices.UpdateShipCart(jsondata);
        }
        /// <summary>
        /// 获取全部出货牌信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GetShipCartAll")]
        public async Task<List<DeviceSelectReturns>> GetShipCartAll(DeviceSelectReturns model)
       {
            return await _agitationServices.GetShipCartAll(model);
        }
        #endregion
        #region 搅拌详细页
        /// <summary>
        /// 当前步骤基础信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "StrstepNameSelecte")]
        public async Task<MessageModel<AgitationStrstepNameSelecteReturns>> StrstepNameSelecte(AgitationStrstepNameSelecteParam jsondata)
        {
            return await _agitationServices.StrstepNameSelecte(jsondata);
        }
        /// <summary>
        /// 物料信息添加
        /// </summary>
        /// <param name="jsondata"></param>
        /// <param name="stepModel"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GetItemInfo")]
        public async Task<MessageModel<AgitationItems>> GetItemInfo(AgitationGetItemInfoParam jsondata)
        {
            return await _agitationServices.GetItemInfo(jsondata);
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "DetailSubmit")]
        public async Task<MessageModel<AgitationDetailModel>> DetailSubmit(AgitationDetailParam jsondata)
        {
            return await _agitationServices.Submit(jsondata);
        }
        #endregion
        /// <summary>
        /// 获取工步名称列表
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GetStepList")]
        public async Task<MessageModel<List<t_gule_step_infoVM>>> GetStepList(t_gule_step_infoVM jsondata)
        {
            return await _agitationServices.GetStepList(jsondata);
        }
    }
}