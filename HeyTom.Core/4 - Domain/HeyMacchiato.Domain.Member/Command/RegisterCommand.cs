using HeyMacchiato.Domain.Member.FluentValidation;
using HeyMacchiato.Infra.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeyMacchiato.Domain.Member.Command
{
	/// <summary>
	/// 注册DTO
	/// </summary>
	public class RegisterCommand
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

		public ResultModel Validate()
		{
			var validate = new RegisterValidation().Validate(this);
			return new ResultModel()
			{
				ResultNo = validate.IsValid ? 1 : 0,
				Message = validate.IsValid ?"Success":string.Join(",", validate.Errors?.Select(x => x.ErrorMessage))
			};
		}
	}
}
