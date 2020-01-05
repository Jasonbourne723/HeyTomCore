using HeyMacchiato.Application.Member.DTO;
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

		/// <summary>
		/// 获取会员信息
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		TResultModel<MemberDTO> GetMemberByEmail(string email);

		/// <summary>
		/// 更新会员信息
		/// </summary>
		/// <param name="updateInfoDTO"></param>
		/// <returns></returns>
		ResultModel UpdateMemberInfo(UpdateInfoDTO updateInfoDTO);

		/// <summary>
		/// 发帖
		/// </summary>
		/// <param name="createPostDTO"></param>
		/// <returns></returns>
		ResultModel CreatePost(CreatePostDTO createPostDTO);

		/// <summary>
		/// 获取帖子列表
		/// </summary>
		/// <param name="memberId"></param>
		/// <returns></returns>
		TResultModel<List<PostDTO>> GetPost(int memberId);
	}
}
