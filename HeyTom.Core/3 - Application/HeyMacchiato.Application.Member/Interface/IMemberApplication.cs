using HeyMacchiato.Domain.Member.DTO;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.Interface
{
	public interface IMemberApplication
	{
		ResultModel Register(RegisterDTO registerDTO);
	}
}
