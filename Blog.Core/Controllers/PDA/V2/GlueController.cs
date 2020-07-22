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
    /// 打胶
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GlueController : ControllerBase
    {
        readonly IGlueServices _glueServices;
        readonly IRedisCacheManager _redisCacheManager;
        readonly IAgitationServices _agitationService;
        public GlueController(IGlueServices glueServices, IRedisCacheManager redisCacheManager, IAgitationServices agitationService)
        {
            _glueServices = glueServices;
            _redisCacheManager = redisCacheManager;
            _agitationService = agitationService;
        }
        /// <summary>
        /// 设备信息-打胶首页
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GlueLoad")]
        public async Task<MessageModel<List<GlueLoadReturns>>> GlueLoad(GlueLoadParam jsondata)
        {
            return await _glueServices.GlueLoad(jsondata);
        }
        /// <summary>
        /// 根据设备获取基础信息-打胶首页
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "DeviceSelectChanges")]
        public async Task<List<DeviceSelectChangesReturns>> DeviceSelectChanges(GlueLoadParam jsondata)
        {
            return await _glueServices.DeviceSelectChanges(jsondata);
        }
        /// <summary>
        /// 获取出货牌-打胶首页
        /// </summary>
        /// <param name="jsondata">{"device_sn":"FJ-DJ-060-02","productType":"正极打胶","productCode":"ZD"}</param>
        /// <returns>{"success":true,"msg":"获取数据成功","response":{"s_number":"01","s_date":"20190730","sys_Date":"20200117","shipCard":"ZD202001170201"}}</returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GetShipCard")]
        public async Task<MessageModel<GetShipCardReturns>> GetShipCard(GetShipCardParam jsondata)
        {
            return await _glueServices.GetShipCard(jsondata);
        }
        /// <summary>
        /// 提交基础信息-打胶首页
        /// </summary>
        /// <param name="jsondata">{"device_sn":"FJ-DJ-060-02","device_id":"string","work_stationname":"string","productType":"正极打胶","productCode":"string","s_number":"string","s_date":"string","sys_Date":"20200117","shipCard":"ZD202001170201","solution_SUM":"120","strLSH":"01","glueUser":"test01","moNumber":"18E-037","solutions_Name":"溶液名称","outQTY":12,"productItem":"string","input_sum":0,"useR_NAME":"test01","item_value":"string"}</param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GlueSubmit")]
        public async Task<MessageModel<GlueSubmitRaturns>> GlueSubmit(GlueSubmitParam jsondata)
        {
            return await _glueServices.GlueSubmit(jsondata);
        }
        /// <summary>
        /// 关结-打胶首页
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GlueClose")]
        public async Task<MessageModel<string>> GlueClose(GlueCloseParam jsondata)
        {
            return await _glueServices.GlueClose(jsondata);
        }

        /// <summary>
        /// 打胶参数录入-页面加载
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "GetCurrentStepLoad")]
        public async Task<GlueDetailModel> GetCurrentStepLoad(GlueDetailParam jsondata)
        {
            return await _glueServices.GetCurrentStepLoad(jsondata);
        }
        /// <summary>
        /// 打胶参数录入-当前步骤值改变事件
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "CurrenStepSelectChanges")]
        public async Task<CurrenStepSelectChangesReturns> CurrenStepSelectChanges(CurrenStepSelectChangesParam jsondata)
        {
            return await _glueServices.CurrenStepSelectChanges(jsondata);
        }
        /// <summary>
        /// 提交信息-打胶参数录入
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "SubmitGlue")]
        public async Task<MessageModel<string>> SubmitGlue(GlueDetailModel jsondata)
        {
            return await _glueServices.SubmitGlue(jsondata);
        }
    }
}
