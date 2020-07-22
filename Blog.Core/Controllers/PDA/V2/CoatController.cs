using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Common;
using Blog.Core.IServices.PDAIServices;
using Blog.Core.Model;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using static Blog.Core.SwaggerHelper.CustomApiVersion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Core.Controllers.PDA.V2
{
    /// <summary>
    /// 涂布
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoatController : Controller
    {
        readonly ICoatServices _coatServices;
        readonly IRedisCacheManager _redisCacheManager;
        public CoatController(ICoatServices coatServices, IRedisCacheManager redisCacheManager)
        {
            _coatServices = coatServices;
            _redisCacheManager = redisCacheManager;
        }
        /// <summary>
        /// 页面加载-设备列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "AddMaterial")]
        public async Task<MessageModel<AgitationItems>> AddMaterial(AddMaterialParam model)
        {
            return await _coatServices.AddMaterial(model);
        }
        /// <summary>
        /// 关结
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "Close")]
        public async Task<MessageModel<string>> Close(CoatCloseParam model)
        {
            return await _coatServices.Close(model);
        }
        /// <summary>
        /// 设备基础信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "DeviceChanged")]
        public async Task<MessageModel<DeviceChangedReturns>> DeviceChanged(DeviceChangedParam model)
        {
            return await _coatServices.DeviceChanged(model);
        }
        /// <summary>
        /// 上节点出货牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "OldShipCartChanged")]
        public async Task<MessageModel<OldShipCartChangedReturns>> OldShipCartChanged(OldShipCartChangedParam model)
        {
            return await _coatServices.OldShipCartChanged(model);
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "Submit")]
        public async Task<MessageModel<string>> Submit(CoatSubmitParam model)
        {
            return await _coatServices.Submit(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "UpdateShipCart")]
        public async Task<MessageModel<string>> UpdateShipCart(CoatUpdateParam model)
        {
            return await _coatServices.UpdateShipCart(model);
        }
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "CheckUserRole")]
        public async Task<MessageModel<CheckUserRoleReturns>> CheckUserRole(CheckUserRoleparam model)
        {
            return await _coatServices.CheckUserRole(model);
        }
        /// <summary>
        /// 获取出货牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GetShipCartCoat")]
        public async Task<MessageModel<GetShipCartCoatReturns>> GetShipCartCoat(GetShipCartCoatParam model)
        {
            return await _coatServices.GetShipCartCoat(model,1);
        }
    }
}
