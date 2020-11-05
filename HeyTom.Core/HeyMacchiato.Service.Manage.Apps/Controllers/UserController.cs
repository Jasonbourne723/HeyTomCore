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
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            { "id","a.Id"},
            { "name","a.Name"},
            { "email","a.Email"},
            { "status","a.Status"},
            { "remark","a.Remark"},
            { "createDate","a.CreateDate"},
            { "icon","a.Icon"},
            { "roleId","b.id"},
            { "roleName","b.name"},
        };

        public UserController(IUserRepository userRepository,
                              IRoleRepository roleRepository,
                              IUserRoleRepository userRoleRepository
            )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult List([FromBody] ViewParam param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {

                var listParm = ConvertRequest.Convert(param, dic);
                result = _userRepository.GetPageResult<UserVModel>(listParm);

            }, true);
        }

        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Detail([FromBody] EmailVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                var user = _userRepository.GetByEmail(model.email);

            }, true);
        }

    }
}
