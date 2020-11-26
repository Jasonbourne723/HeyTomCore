using HeyMacchiato.Infra.Jwt;
using HeyMacchiato.Infra.MvcCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeyMacchiato.Infra.Filter
{
    public class JwtAuthorziationActionAttribute : ActionFilterAttribute
    {
        TokenValidationParameters _tokenValidationParameters;
        public JwtAuthorziationActionAttribute(TokenValidationParameters tokenValidationParameters)
        {
            _tokenValidationParameters = tokenValidationParameters;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var headers = context.HttpContext.Request.Headers;
                var controller = (context.Controller as BaseController);
                if (controller != null && controller._authType)
                {
                    if (!headers.ContainsKey("Authorization"))
                    {
                        context.Result = new ContentResult()
                        {
                            Content = JsonConvert.SerializeObject(new
                            {
                                Message = "权限未通过"
                            }),
                            StatusCode = 401,
                            ContentType = "application/json;charset=utf-8",
                        };
                        return;
                    }
                    var tokenStr = headers["Authorization"].ToString();
                    if (!tokenStr.StartsWith("Bearer "))
                    {
                        context.Result = new ContentResult()
                        {
                            Content = JsonConvert.SerializeObject(new
                            {
                                Message = "权限未通过"
                            }),
                            StatusCode = 401,
                            ContentType = "application/json;charset=utf-8",
                        };
                        return;
                    }
                    tokenStr = tokenStr.Substring("Bearer ".Length).Trim();

                    var jwtToken = JwtHelper.SerializeJwt(tokenStr, _tokenValidationParameters);
                    var payLoad = jwtToken.Payload;
                    var expireDate = GetDateTime(payLoad.Claims.FirstOrDefault(x => x.Type == "exp").Value);
                    if (expireDate < DateTime.Now)
                    {
                        context.Result = new ContentResult()
                        {
                            Content = JsonConvert.SerializeObject(new
                            {
                                Message = "权限已过期"
                            }),
                            StatusCode = 401,
                            ContentType = "application/json;charset=utf-8",
                        };
                        return;
                    }
                    var userId = int.Parse(payLoad.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                    var userName = payLoad.Claims.FirstOrDefault(x => x.Type == "userName").Value;
                    var email = payLoad.Claims.FirstOrDefault(x => x.Type == "email").Value;
                    controller._claimEntity = new ClaimEntity(userId, userName, email);
                }
                base.OnActionExecuting(context);
            }
            catch (Exception ex)
            {
                context.Result = new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(new
                    {
                        Message = "权限未通过"
                    }),
                    StatusCode = 401,
                    ContentType = "application/json;charset=utf-8",
                };
                return;
            }

        }


        /// <summary>  
		/// 时间戳Timestamp转换成日期  
		/// </summary>  
		/// <param name="timeStamp"></param>  
		/// <returns></returns>  
		private DateTime GetDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return dtStart.Add(toNow);
        }
    }

    public class NoAuthorizationActionAttribute : ActionFilterAttribute
    {
        public NoAuthorizationActionAttribute()
        {
            base.Order = -1;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            var controller = (context.Controller as BaseController);
            controller._authType = false;
        }
    }
}
