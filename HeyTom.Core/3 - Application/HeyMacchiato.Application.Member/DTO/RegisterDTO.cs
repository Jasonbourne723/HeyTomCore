using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.DTO
{
	/// <summary>
	/// 注册DTO
	/// </summary>
	public class RegisterDTO
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
	}
}
