using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Application.Member.Interface;
using HeyMacchiato.Application.Member.DTO;
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
		public IActionResult Post([FromBody]RegisterDTO registerViewModel)
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
		/// <param name="emailDTO"></param>
		/// <returns></returns>
		[Route("[action]")]
		[HttpPost]
		[ProducesDefaultResponseType(typeof(TResultModel<VerificationCodelDTO>))]
		public IActionResult VerificationCode([FromBody] EmailDTO emailDTO)
		{
			var result = new ResultModel(1);
			return this.Wrapper(ref result, () =>
			{
				result = _memberApplication.SendRegisterVerificaionCode(emailDTO);
			}, true);
		}
	}
}