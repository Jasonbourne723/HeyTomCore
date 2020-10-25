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

		private string message;
		public string Message
		{
			get
			{ 
				if (ResultNo > 0)
				{
					return "Success";
				}
				else
				{
					return message;
				}
			}
			set
			{
				message = value;
			}
		}

		public bool IsSuccess => (ResultNo > 0);
	}
}
