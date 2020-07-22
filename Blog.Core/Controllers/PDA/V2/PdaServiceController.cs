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
using Microsoft.AspNetCore.Mvc;
using static Blog.Core.SwaggerHelper.CustomApiVersion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Core.Controllers.PDA.V2
{
    /// <summary>
    /// 公共接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PdaServiceController : Controller
    {
        readonly IGlueServices _glueServices;
        readonly IAgitationServices _agitationServices;
        readonly IRedisCacheManager _redisCacheManager;
        readonly IAgitationServices _agitationService;
        readonly IPdaService _pdaService;
        public PdaServiceController(IPdaService pdaService,IAgitationServices agitationServices,IGlueServices glueServices, IRedisCacheManager redisCacheManager)
        {
            _glueServices = glueServices;
            _agitationServices = agitationServices;
            _redisCacheManager = redisCacheManager;
            _pdaService = pdaService;
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
        /// 设备信息
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2, "DeviceLoad")]
        public async Task<MessageModel<List<GlueLoadReturns>>> DeviceLoad(GlueLoadParam jsondata)
        {
            return await _glueServices.GlueLoad(jsondata);
        }
        /// <summary>
        /// PDA版本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomRoute(ApiVersions.V2, "VersionInfo")]
        public async Task<MessageModel<List<APP_VersionModel>>> VersionInfo()
        {
            return await _pdaService.VersionInfo();
        }
    }
}
