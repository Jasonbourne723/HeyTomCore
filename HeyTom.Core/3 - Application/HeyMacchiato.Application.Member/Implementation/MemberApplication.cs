using HeyMacchiato.Application.Member.Interface;
using HeyMacchiato.Domain.Member.DTO;
using HeyMacchiato.Domain.Member.Service;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.Implementation
{
	public class MemberApplication : IMemberApplication
	{
		private readonly IMemberService memberService;

		public MemberApplication(IMemberService memberService)
		{
			this.memberService = memberService;
		}

		public ResultModel Register(RegisterDTO registerDTO)
		{
			return memberService.Register(registerDTO);
		}
	}
}
