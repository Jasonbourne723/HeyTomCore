using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Core.Command
{
	public interface ICommand
	{
		ResultModel Validate();
	}
}
