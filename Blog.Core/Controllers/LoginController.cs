﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Core.AuthHelper;
using Blog.Core.AuthHelper.OverWrite;
using Blog.Core.Common.Helper;
using Blog.Core.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {

        readonly ISysUserInfoServices _sysUserInfoServices;
        readonly IUserRoleServices _userRoleServices;
        readonly IRoleServices _roleServices;
        readonly PermissionRequirement _requirement;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="sysUserInfoServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="roleServices"></param>
        /// <param name="requirement"></param>
        public LoginController(ISysUserInfoServices sysUserInfoServices, IUserRoleServices userRoleServices, IRoleServices roleServices, PermissionRequirement requirement)
        {
            this._sysUserInfoServices = sysUserInfoServices;
            this._userRoleServices = userRoleServices;
            this._roleServices = roleServices;
            _requirement = requirement;
        }
        #region 获取token的第1种方法
        /// <summary>
        /// 获取JWT的方法1
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public async Task<object> GetJwtStr(string name, string pass)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作

            if (name != null)
            {
                if (pass == "123")
                    jwtStr = "Admin";
                else
                    jwtStr = "Client";
                TokenModelJwt tokenModel = new TokenModelJwt { Uid =long.Parse(name), Role = jwtStr };

                jwtStr = JwtHelper.IssueJwt(tokenModel);
                suc = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }

            return Ok(new
            {
                success = suc,
                token = jwtStr
            });
        }


        /// <summary>
        /// 获取JWT的方法2：给Nuxt提供
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTokenNuxt")]
        public object GetJwtStrForNuxt(string name, string pass)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了
            if (name == "admins" && pass == "admins")
            {
                TokenModelJwt tokenModel = new TokenModelJwt
                {
                    Uid = 1,
                    Role = "Admin"
                };

                jwtStr = JwtHelper.IssueJwt(tokenModel);
                suc = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }
            var result = new
            {
                data = new { success = suc, token = jwtStr }
            };

            return Ok(new
            {
                success = suc,
                data = new { success = suc, token = jwtStr }
            });
        }
        #endregion

        /// <summary>
        /// 获取JWT的方法3：整个系统主要方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("JWTToken3.0")]
        public async Task<object> GetJwtToken3(string name = "", string pass = "")
        {
            string jwtStr = string.Empty;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass))
            {
                return new JsonResult(new
                {
                    Status = false,
                    message = "用户名或密码不能为空"
                });
            }

            pass = MD5Helper.MD5Encrypt32(pass);

            var user = await _sysUserInfoServices.Query(d => d.uLoginName == name && d.uLoginPWD == pass);
            if (user.Count > 0)
            {
                var userRoles = await _sysUserInfoServices.GetUserRoleNameStr(name, pass);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(JwtRegisteredClaimNames.Jti, user.FirstOrDefault().uID.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                return new JsonResult(token);
            }
            else
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "认证失败"
                });
            }

        }

        /// <summary>
        /// 获取JWT的方法4：给 JSONP 测试
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="id"></param>
        /// <param name="sub"></param>
        /// <param name="expiresSliding"></param>
        /// <param name="expiresAbsoulute"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("jsonp")]
        public void Getjsonp(string callBack, long id = 1, string sub = "Admin", int expiresSliding = 30, int expiresAbsoulute = 30)
        {
            TokenModelJwt tokenModel = new TokenModelJwt
            {
                Uid = id,
                Role = sub
            };

            string jwtStr = JwtHelper.IssueJwt(tokenModel);

            string response = string.Format("\"value\":\"{0}\"", jwtStr);
            string call = callBack + "({" + response + "})";
            Response.WriteAsync(call);
        }

    }
}