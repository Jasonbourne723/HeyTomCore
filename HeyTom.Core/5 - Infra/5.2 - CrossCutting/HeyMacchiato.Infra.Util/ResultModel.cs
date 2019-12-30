using System;

namespace HeyMacchiato.Infra.Util
{
	public class ResultModel
	{
		public ResultModel(int resultNo, string message)
		{
			ResultNo = resultNo;
			Message = message;
		}
		public ResultModel(int resultNo)
		{
			ResultNo = resultNo;
		}

		public ResultModel()
		{
			ResultNo = 1;
		}

		public int ResultNo { get; set; }

		public string Message { get; set; }

		public bool IsSuccess => (ResultNo > 0);
	}
}
