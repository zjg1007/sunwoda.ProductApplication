using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Blog.Core.Common;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Model.PDAModel;
using Blog.Core.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMESWeb;
using static Blog.Core.SwaggerHelper.CustomApiVersion;

namespace Blog.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        PMESWeb.MESInterfaceSoapClient  pmes= new PMESWeb.MESInterfaceSoapClient(PMESWeb.MESInterfaceSoapClient.EndpointConfiguration.MESInterfaceSoap);
        readonly IInterface_LogServices _Interface_LogServices;
        readonly IRedisCacheManager _redisCacheManager;

        public ValuesController(IInterface_LogServices Interface_LogServices, IRedisCacheManager redisCacheManager)
        {
            this._Interface_LogServices = Interface_LogServices;
            this._redisCacheManager = redisCacheManager;
        }
        // GET: api/Values
        /// <summary>
        /// v1版本的接口信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        //[Authorize(Roles ="Admin,Client")]

        //[Authorize(Policy = "SystemOrAdmin")]
        [ApiExplorerSettings(GroupName = "V1")]
        public async  Task<object> Get()
        {
            //List<Interface_Log> tables = pmes.GetSqlData(@"select * from interface_log").DataToXml<Interface_Log>();
            //List<Interface_Log> result =  await _Interface_LogServices.GetTestInterfaceLogData();

            var result = await _Interface_LogServices.SqlQuery<CheckNo_Login>(@" select s.project_id,
        (select distinct p.cell_model
         from t_co_package p
         where p.packagenumber = @packagenumber) cell_model
 from t_pm_project_base s, t_pm_mo_base n
 where  n.project_id = s.project_base_id
 and    n.close_flag in (2, 4)
 and    n.mo_number = @packagenumber", new { packagenumber = "18E-012" });
            return Ok(new
            {
                success = true,
                data = result
            }); ;
        }
        /// <summary>
        /// v2版本的接口信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = "SystemOrAdmin")]
        //MVC自带特性 对 api 进行组管理
        //[ApiExplorerSettings(GroupName = "V2")]
        //路径 如果以 / 开头，表示绝对路径，反之相对 controller 的想u地路径
        //[Route("/api/V2/Values/V2_Get")]


        //[AllowAnonymous]//不受授权控制，任何人都可访问


        //和上边的版本控制以及路由地址都是一样的
        [CustomRoute(ApiVersions.V2, "V2_value2")]
        public IEnumerable<string> V2_Get()
        {
            //throw new Exception("my text info");
            return new string[] { "V2_value1", "V2_value2" };
        }
        //[Authorize]
        // GET: api/Values/5
        //[CustomRoute(ApiVersions.V2, "V2_value2")]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            //throw new Exception("爆炸了");
            return "value";
        }
        //[Authorize(Policy = "Client")]
        // POST: api/Values
        [HttpPost]
        public object Post(CheckNo_Login jsondata)
        {
            return Ok(new
            {
                success = true,
                token = "成功访问POst",
                data = jsondata
            });
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
