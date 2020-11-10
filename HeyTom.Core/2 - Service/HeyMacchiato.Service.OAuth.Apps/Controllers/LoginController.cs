using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HeyMacchiato.Infra.Encryption.Md5;
using HeyMacchiato.Infra.Filter;
using HeyMacchiato.Infra.Jwt;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.OAuth.Apps.Models;
using HeyTom.Manage.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace HeyMacchiato.Service.OAuth.Apps.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly PermissionRequirement _permissionRequirement;

        public LoginController(IUserRepository userRepository,
                                PermissionRequirement permissionRequirement)
        {
            _userRepository = userRepository;
            _permissionRequirement = permissionRequirement;
        }


        /// <summary>
        /// 邮箱登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesDefaultResponseType(typeof(TResultModel<TokenVModel>))]
        [NoAuthorizationAction]
        public IActionResult Get(string email, string password)
        {
            var result = new TResultModel<TokenVModel>(1);
            return this.Wrapper(ref result, () =>
            {
                var md5Password = Md5Helper.GenerateMD5(password);

                var user = _userRepository.GetByEmail(email);
                if (user == null || md5Password != user.Pwd)
                {
                    result.ResultNo = -1;
                    result.Message = "用户名或密码错误";
                    return;
                }
                else
                {
                    var jwtstr = JwtHelper.BuildJwtToken(new Claim[3]{
                           // new Claim(ClaimTypes.Role,"admin"),
                            new Claim("userName",user.Name),
                            new Claim("userId",user.Id.ToString()),
                            new Claim("email",user.Email),
                        }, _permissionRequirement);
                    result.TModel = new TokenVModel()
                    {
                        token = jwtstr
                    };
                }
            }, false);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<UserInfoVModel>))]
        public IActionResult GetUserInfo()
        {
            var result = new TResultModel<UserInfoVModel>(1);
            return this.Wrapper(ref result, () =>
            {
                var userId = this._claimEntity.userId;
                var user = _userRepository.GetById(userId);
                if (user == null)
                {
                    result.ResultNo = -1;
                    result.Message = "用户信息有误";
                    return;
                }
                result.TModel = new UserInfoVModel()
                {
                    email = user.Email,
                    id = user.Id,
                    name = user.Name,
                    remark = user.Remark
                };
            }, true);
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