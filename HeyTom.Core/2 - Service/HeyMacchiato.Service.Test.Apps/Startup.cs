using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HeyMacchiato.Infra.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HeyMacchiato.Service.Test.Apps
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			#region  Jwt
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs")),//参数配置在下边
				ValidateIssuer = true,
				ValidIssuer = "Jasonbourne",//发行人
				ValidateAudience = true,
				ValidAudience = "HeyMacchiato",//订阅人
				ValidateLifetime = true,
				ClockSkew = TimeSpan.FromMinutes(10),
				RequireExpirationTime = true,
			};
			var permission = new List<PermissionItem> {
							   new PermissionItem {  Url="/", Name="Admin"},
							   new PermissionItem {  Url="/api/values", Name="Admin"},
							   new PermissionItem {  Url="/", Name="system"},
							   new PermissionItem {  Url="/api/values1", Name="system"}
			};
			var permissionRequirement = new PermissionRequirement(
				"/api/denied",// 拒绝授权的跳转地址（目前无用）
				permission,//权限集合
				ClaimTypes.Role,//基于角色的授权
				"Jasonbourne",//发行人
				"HeyMacchiato",//订阅人
				 new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs")), SecurityAlgorithms.HmacSha256),//签名凭据
				expiration: TimeSpan.FromSeconds(300)//接口的过期时间，注意这里没有了缓冲时间，你也可以自定义，在上边的TokenValidationParameters的 ClockSkew
			);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(o =>
			{
				o.TokenValidationParameters = tokenValidationParameters;
			});
			#endregion
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
