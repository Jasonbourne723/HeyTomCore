using HeyMacchiato.Domain.Member.FluentValidation;
using HeyMacchiato.Infra.Util;
using System;
using System.Linq;

namespace HeyMacchiato.Domain.Member.Command
{
	public class UpdateInfoCommand
	{
		public int Id { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 昵称
		/// </summary>
		public string NickName { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public short Sex { get; set; }
		/// <summary>
		/// 生日
		/// </summary>
		public DateTime Birthday { get; set; }

		public ResultModel Validate()
		{
			var validate = new UpdateInfoValidation().Validate(this);
			return new ResultModel()
			{
				ResultNo = validate.IsValid ? 1 : 0,
				Message = validate.IsValid ? "Success" : string.Join(",", validate.Errors?.Select(x => x.ErrorMessage))
			};
		}
	}
}
