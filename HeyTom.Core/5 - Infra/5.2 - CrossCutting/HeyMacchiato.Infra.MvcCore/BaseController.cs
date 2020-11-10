using HeyMacchiato.Infra.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace HeyMacchiato.Infra.MvcCore
{
	public class BaseController : ControllerBase
	{
		public BaseController()
		{
			_authType = true;
		}

		public bool _authType;

		public ClaimEntity _claimEntity { get; set; }

		protected IActionResult Wrapper(ref ResultModel result, Action action, bool isVaild)
		{
			if (isVaild)
			{
				if (!ModelState.IsValid)
				{
					result.ResultNo = -1;
					result.Message = string.Join(",", ModelState.Keys.Select(x => ModelState[x].Errors[0].ErrorMessage)); 
					return new ContentResult()
					{
						Content = JsonConvert.SerializeObject(result),
						ContentType = "application/json;charset=utf-8",
					};
				}
			}
			try
			{
				action();
			}
			catch (Exception ex)
			{
				result.ResultNo = -1;
				result.Message = "请求失败,请稍后重试 : " + ex.Message;
			}
			return new ContentResult()
			{
				Content = JsonConvert.SerializeObject(result),
				ContentType = "application/json;charset=utf-8",
			};
		}

		protected IActionResult Wrapper<T>(ref TResultModel<T> result, Action action, bool isVaild)
		{
			if (isVaild)
			{
				if (!ModelState.IsValid)
				{
					result.ResultNo = -1;
					result.Message = string.Join(",", ModelState.Keys.Select(x => ModelState[x].Errors[0].ErrorMessage));
					return new ContentResult()
					{
						Content = JsonConvert.SerializeObject(result),
						ContentType = "application/json;charset=utf-8",
					};
				}
			}
			try
			{
				action();
			}
			catch (Exception ex)
			{
				result.ResultNo = -1;
				result.Message = "请求失败,请稍后重试 : "+ex.Message;
			}
			return new ContentResult()
			{
				Content = JsonConvert.SerializeObject(result),
				ContentType = "application/json;charset=utf-8",
			};
		}

	}

	public class ClaimEntity
	{
        public ClaimEntity(int userId, string userName, string email)
        {
            this.userId = userId;
            this.userName = userName;
            this.email = email;
        }

        public int userId { get;  }

        public string userName { get;  }


        public string email { get; }


    }
}
