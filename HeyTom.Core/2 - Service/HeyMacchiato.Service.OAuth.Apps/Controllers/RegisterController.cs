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
    /// 注册控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : BaseController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RegisterController()
        {
        }


        /// <summary>
        /// 邮箱注册
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Post([FromBody]string a)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result,()=> { 
            
            },true);
        }
    }
}