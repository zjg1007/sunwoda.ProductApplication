using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Common;
using Blog.Core.IServices;
using Blog.Core.IServices.PDAIServices;
using Blog.Core.Model;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.SwaggerHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Blog.Core.SwaggerHelper.CustomApiVersion;

namespace Blog.Core.Controllers.PDA.V2
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly ILoginServices _loginServices;
        readonly IRedisCacheManager _redisCacheManager;

        public LoginController(ILoginServices loginServices, IRedisCacheManager redisCacheManager)
        {
            _loginServices = loginServices;
            _redisCacheManager = redisCacheManager;
        }
        /// <summary>
        /// PDA用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CustomRoute(ApiVersions.V2,"Login")]
        public async Task<MessageModel<string>> Login(LoginParam jsondata)
        {
            return await _loginServices.Login(jsondata);
        }
    }
}