using HeyMacchiato.Domain.Member.Command;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Member.Service
{
	public interface IMemberService
	{
		/// <summary>
		/// 邮箱注册
		/// </summary>
		/// <param name="registerCommand"></param>
		/// <returns></returns>
		ResultModel Register(RegisterCommand registerCommand);
		/// <summary>
		/// 更新个人资料
		/// </summary>
		/// <param name="updateInfoCommand"></param>
		/// <returns></returns>
		ResultModel UpdateInfo(UpdateInfoCommand updateInfoCommand);

		/// <summary>
		/// 发布帖子
		/// </summary>
		/// <param name="postCommand"></param>
		/// <returns></returns>
		ResultModel Post(PostCommand postCommand);
	}
}
