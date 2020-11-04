using HeyMacchiato.Infra.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace HeyMacchiato.Infra.MvcCore
{
	public class BaseController : ControllerBase
	{
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
}
