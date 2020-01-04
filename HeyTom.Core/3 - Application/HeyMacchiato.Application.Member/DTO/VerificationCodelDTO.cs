using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HeyMacchiato.Application.Member.ViewModel
{
	/// <summary>
	/// 
	/// </summary>
	public class VerificationCodelDTO
	{
		/// <summary>
		/// 验证码
		/// </summary>
		public string VerificationCode { get; set; }
	}

	public class EmailDTO
	{
		/// <summary>
		/// 邮箱
		/// </summary>
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "邮箱")]
		[EmailAddress]
		public string Email { get; set; }
	}
}
