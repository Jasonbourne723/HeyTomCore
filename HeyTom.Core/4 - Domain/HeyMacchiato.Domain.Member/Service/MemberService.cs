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
		/// <param name="registerDTO"></param>
		/// <returns></returns>
		public ResultModel Register(RegisterDTO registerDTO)
		{
			var result = registerDTO.Validate();
			if (!result.IsSuccess) return result;
			if (_memberRepository.GetByEmail(registerDTO.Email) != null)
			{
				result.Message = "";
				return new ResultModel()
				{
					ResultNo = -1,
					Message = "该邮箱已经注册过账号,不能重复使用"
				};
			}
			var member = new Models.Member(registerDTO.NickName, registerDTO.Email, registerDTO.Password);
			return _memberRepository.Add(member);
		}
	}
}
