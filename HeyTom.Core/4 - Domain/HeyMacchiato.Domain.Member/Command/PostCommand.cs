using HeyMacchiato.Domain.Core.Command;
using HeyMacchiato.Domain.Member.FluentValidation;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeyMacchiato.Domain.Member.Command
{
	public class PostCommand : ICommand
	{
		public int MemberId { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 类型 0：动态 1：领养
		/// </summary>
		public short Type { get; set; }

		public string Image1 { get; set; }

		public string Image2 { get; set; }

		public string Image3 { get; set; }

		public ResultModel Validate()
		{
			var validate = new PostValidation().Validate(this);
			return new ResultModel()
			{
				ResultNo = validate.IsValid ? 1 : 0,
				Message = validate.IsValid ? "Success" : string.Join(",", validate.Errors?.Select(x => x.ErrorMessage))
			};
		}
	}
}
