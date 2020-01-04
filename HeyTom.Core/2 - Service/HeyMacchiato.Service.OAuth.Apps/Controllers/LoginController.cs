using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HeyMacchiato.Application.Member.DTO;
using HeyMacchiato.Application.Member.Interface;
using HeyMacchiato.Infra.Encryption.Md5;
using HeyMacchiato.Infra.Jwt;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeyMacchiato.Service.OAuth.Apps.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IMemberApplication _memberApplication;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginController(IMemberApplication memberApplication)
        {
            this._memberApplication = memberApplication;
        }
        /// <summary>
        /// 邮箱登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesDefaultResponseType(typeof(TokenDTO))]
        public IActionResult Get(string email,string password)
        {
            var result = new TResultModel<TokenDTO>(1);
            return this.Wrapper(ref result,()=> {
                result = _memberApplication.Login(new EmailLoginDTO() { Email = email,Password = password});
            },false);
        }

        /// <summary>
        /// 测试token
        /// </summary>
        /// <returns></returns>
       [HttpGet]
       [Route("[action]")]
       [ProducesDefaultResponseType(typeof(string))]
       [Authorize]
        public string Test()
        {
            return "success";
        }
	}
}