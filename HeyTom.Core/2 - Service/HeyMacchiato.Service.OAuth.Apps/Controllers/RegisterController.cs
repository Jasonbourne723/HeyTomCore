using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Application.Member.Interface;
using HeyMacchiato.Application.Member.ViewModel;
using HeyMacchiato.Infra.CsRedis;
using HeyMacchiato.Infra.Email;
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
		private readonly IMemberApplication _memberApplication;

		/// <summary>
		/// 构造函数
		/// </summary>
		public RegisterController(IMemberApplication memberApplication)
		{
			this._memberApplication = memberApplication;
		}


		/// <summary>
		/// 邮箱注册
		/// </summary>
		/// <param name="registerViewModel"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesDefaultResponseType(typeof(ResultModel))]
		public IActionResult Post([FromBody]RegisterViewModel registerViewModel)
		{
			var result = new ResultModel();
			return this.Wrapper(ref result, () =>
			{
				result = _memberApplication.Register(registerViewModel);
			}, true);
		}

		/// <summary>
		/// 发送注册验证码
		/// </summary>
		/// <param name="emailViewModel"></param>
		/// <returns></returns>
		[Route("[action]")]
		[HttpPost]
		[ProducesDefaultResponseType(typeof(TResultModel<VerificationCodelViewModel>))]
		public IActionResult VerificationCode([FromBody] EmailViewModel emailViewModel)
		{
			var result = new ResultModel(1);
			return this.Wrapper(ref result, () =>
			{
				var key = $"VerificationCodel:Email:{emailViewModel.Email}";
				var toEmail = new List<string>() { emailViewModel.Email };
				var random = new Random();
				var code = random.Next(1000, 10000);

				new CsRedisBase().set(key, code.ToString(), 120);

				var subject = "Hey Macchiato 注册验证码";
				var content = $"您的验证码是{code.ToString()},有效期120秒!";
				result = EmailHelper.Send(toEmail, subject, content, "Hey Macchiato");
			}, true);
		}
	}
}