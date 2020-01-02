using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.ViewModel
{
	public class RegisterViewModel
	{
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// 昵称
		/// </summary>
		public string NickName { get; set; }
		/// <summary>
		/// 验证码
		/// </summary>
		public string  VerificationCode{ get; set; }
	}
}
