using AutoMapper;
using HeyMacchiato.Application.Member.Interface;
using HeyMacchiato.Application.Member.ViewModel;
using HeyMacchiato.Domain.Member.DTO;
using HeyMacchiato.Domain.Member.Service;
using HeyMacchiato.Infra.CsRedis;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.Implementation
{
	public class MemberApplication : IMemberApplication
	{
		private readonly IMemberService memberService;
		private readonly IMapper _mapper;

		public MemberApplication(IMemberService memberService,
											IMapper mapper)
		{
			this.memberService = memberService;
			this._mapper = mapper;
		}

		public ResultModel Register(RegisterViewModel registerViewModel)
		{
			var key = $"VerificationCodel:Email:{registerViewModel.Email}"; 
			var localVerificationCode = new CsRedisBase().Get(key);
			if (string.IsNullOrWhiteSpace(localVerificationCode))
			{
				return new ResultModel(-1,"未发送验证码或验证码已过期");
			}
			if (localVerificationCode != registerViewModel.VerificationCode)
			{
				return new ResultModel(-1, "验证码错误");
			}
			var registerDTO = _mapper.Map<RegisterDTO>(registerViewModel);
			return memberService.Register(registerDTO);
		}
	}
}
