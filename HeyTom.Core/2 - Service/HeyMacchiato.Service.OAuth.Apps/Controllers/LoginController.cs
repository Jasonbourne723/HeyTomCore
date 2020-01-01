using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
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
        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginController()
        {
        }

        /// <summary>
        /// 邮箱登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Get(string email,string password,string redirectUrl)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result,()=> { 
            
            },false);
        }
    }
}