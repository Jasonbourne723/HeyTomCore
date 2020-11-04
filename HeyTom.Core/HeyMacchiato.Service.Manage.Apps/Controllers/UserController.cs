using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.Manage.Apps.Models;
using HeyTom.Manage.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeyMacchiato.Service.Manage.Apps.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult List()
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () => {

            }, true);
        }

        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Detail([FromBody ] EmailVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () => {
                var user = _userRepository.GetByEmail(model.email);

            }, true);
        }
    }
}
