using HeyMacchiato.Domain.Member.DTO;
using HeyMacchiato.Domain.Member.Repository;
using HeyMacchiato.Infra.Util;

namespace HeyMacchiato.Domain.Member.Service
{
	public class MemberService : IMemberService
	{
		private readonly IMemberRepository _memberRepository;

		public MemberService(IMemberRepository memberRepository)
		{
			this._memberRepository = memberRepository;
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
			return _memberRepository.Add(member);
		}
	}
}
