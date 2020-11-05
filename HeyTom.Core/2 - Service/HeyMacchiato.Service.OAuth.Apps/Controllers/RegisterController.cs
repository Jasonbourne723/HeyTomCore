using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.Email;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HeyMacchiato.Service.OAuth.Apps.Models;
using HeyMacchiato.Infra.Encryption.Md5;
using HeyTom.Manage.Repository;

namespace HeyMacchiato.Service.OAuth.Apps.Controllers
{
    /// <summary>
    /// 注册控制器
    /// </summary>
    [Route("api/[controller]")]
    public class RegisterController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public RegisterController(IUserRepository userRepository,
                                    IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }



        /// <summary>
        /// 邮箱注册
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Post([FromBody] RegisterVModel model)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                var user = _userRepository.GetByEmail(model.email);
                if (user != null)
                {
                    result.ResultNo = -1;
                    result.Message = "该邮箱已经注册过账号";
                    return;
                }
                var key = $"VerificationCodel:Email:{model.email}";
                if ("3197" != model.verificationCode)
                {
                    result.ResultNo = -1;
                    result.Message = "验证码错误";
                    return;
                }
                var pwd = Md5Helper.GenerateMD5(model.password);
                var newUser = new HeyTom.Manage.Model.User()
                {
                    Email = model.email,
                    Icon = "",
                    Name = model.nickName,
                    Pwd = pwd,
                    Remark = "",
                    Status = 1,
                    CreateDate = DateTime.Now
                };
                result = _userRepository.Add(newUser);
                _userRoleRepository.Add(new HeyTom.Manage.Model.UserRole() { 
                    RoleId = 4,
                    UserId = newUser.Id
                });

            }, true);
        }

        /// <summary>
        /// 发送注册验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        [ProducesDefaultResponseType(typeof(TResultModel<VerificationCodeVModel>))]
        public IActionResult VerificationCode([FromBody] EmailVModel model)
        {
            var result = new ResultModel(1);
            return this.Wrapper(ref result, () =>
            {
                var user = _userRepository.GetByEmail(model.email);
                if (user != null)
                {
                    result.ResultNo = -1;
                    result.Message = "该邮箱已经注册过账号";
                    return;
                }
                // var key = $"VerificationCodel:Email:{emailDTO.email}";
                var toEmail = new List<string>() { model.email };
                var random = new Random();
                var code = random.Next(1000, 10000);

                //  new CsRedisBase().set(key, code.ToString(), 120);

                var subject = "HeyTom 注册验证码";
                var content = $"您的验证码是{"3197"},有效期120秒!";
                result = EmailHelper.Send(toEmail, subject, content);
            }, true);
        }
    }
}