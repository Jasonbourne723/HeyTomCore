using HeyMacchiato.Domain.Member.DTO;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Member.Service
{
	public interface IMemberService
	{
		ResultModel Register(RegisterDTO registerDTO);
	}
}
