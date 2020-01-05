using HeyMacchiato.Domain.Member.Command;
using HeyMacchiato.Domain.Member.Repository;
using HeyMacchiato.Infra.Util;

namespace HeyMacchiato.Domain.Member.Service
{
	public class MemberService : IMemberService
	{
		private readonly IMemberRepository _memberRepository;
		private readonly IPostRepository _postRepository;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="memberRepository"></param>
		public MemberService(IMemberRepository memberRepository,
									IPostRepository postRepository)
		{
			this._memberRepository = memberRepository;
			this._postRepository = postRepository;
		}
		/// <summary>
		/// 发布帖子
		/// </summary>
		/// <param name="postCommand"></param>
		/// <returns></returns>
		public ResultModel Post(PostCommand postCommand)
		{
			var result = postCommand.Validate();
			if (!result.IsSuccess) return result;
			var member = _memberRepository.GetById(postCommand.MemberId);
			if (member == null)
			{
				return new ResultModel(-1, "会员不存在");
			}
			var post =  member.Post(postCommand.Title, postCommand.Content, postCommand.Type, postCommand.Image1, postCommand.Image2, postCommand.Image3);
			result = _postRepository.Add(post);
			//发布事件
			return result;
		}
		/// <summary>
		/// 邮箱注册
		/// </summary>
		/// <param name="registerCommand"></param>
		/// <returns></returns>
		public ResultModel Register(RegisterCommand registerCommand)
		{
			var result = registerCommand.Validate();
			if (!result.IsSuccess) return result;
			if (_memberRepository.GetByEmail(registerCommand.Email) != null)
			{
				result.Message = "";
				return new ResultModel()
				{
					ResultNo = -1,
					Message = "该邮箱已经注册过账号,不能重复使用"
				};
			}
			var member = new Models.Member(registerCommand.NickName, registerCommand.Email, registerCommand.Password);
			result =  _memberRepository.Add(member);
			//发布事件
			return result;
		}
		/// <summary>
		/// 更新个人资料
		/// </summary>
		/// <param name="updateInfoCommand"></param>
		/// <returns></returns>
		public ResultModel UpdateInfo(UpdateInfoCommand updateInfoCommand)
		{
			var result = updateInfoCommand.Validate();
			if (!result.IsSuccess) return result;
			var member = _memberRepository.GetById(updateInfoCommand.Id);
			if (member == null)
			{
				return new ResultModel(-1,"会员不存在");
			}
			member.UpdateInfo(updateInfoCommand.Name, updateInfoCommand.NickName, updateInfoCommand.Sex,updateInfoCommand.Birthday);
			result =  _memberRepository.Update(member);
			//发布事件
			return result;
		}
	}
}
