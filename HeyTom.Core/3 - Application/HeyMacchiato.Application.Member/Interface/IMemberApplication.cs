using HeyMacchiato.Application.Member.DTO;
using HeyMacchiato.Application.Member.ViewModel;
using HeyMacchiato.Domain.Member.DTO;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.Interface
{
	public interface IMemberApplication
	{
		/// <summary>
		/// 邮箱注册
		/// </summary>
		/// <param name="registerDTO"></param>
		/// <returns></returns>
		ResultModel Register(RegisterDTO registerDTO);
		/// <summary>
		/// 邮箱登录
		/// </summary>
		/// <param name="emailLoginDTO"></param>
		/// <returns></returns>
		TResultModel<TokenDTO> Login(EmailLoginDTO emailLoginDTO);

		/// <summary>
		/// 发送注册验证码
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		ResultModel SendRegisterVerificaionCode(EmailDTO emailDTO);
	}
}
