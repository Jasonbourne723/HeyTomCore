using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeyMacchiato.Service.OAuth.Apps.Models
{
    public class TokenVModel
    {
		/// <summary>
		/// token
		/// </summary>
		public string token { get; set; }
	}


	public class UserInfoVModel
	{
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string remark { get; set; }
    }

	public class RegisterVModel
	{
		/// <summary>
		/// 邮箱
		/// </summary>
		public string email { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string password { get; set; }
		/// <summary>
		/// 昵称
		/// </summary>
		public string nickName { get; set; }
		/// <summary>
		/// 验证码
		/// </summary>
		public string verificationCode { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class VerificationCodeVModel
	{
		/// <summary>
		/// 验证码
		/// </summary>
		public string verificationCode { get; set; }
	}

	public class EmailVModel
	{
		/// <summary>
		/// 邮箱
		/// </summary>
		[Required(ErrorMessage = "{0}不能为空")]
		[Display(Name = "邮箱")]
		[EmailAddress]
		public string email { get; set; }
	}
}
