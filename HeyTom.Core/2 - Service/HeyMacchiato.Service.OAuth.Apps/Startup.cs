using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HeyMacchiato.Infra.MvcCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using HeyMacchiato.Infra.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Runtime.Loader;
using System.Reflection;

namespace HeyMacchiato.Service.OAuth.Apps
{
	/// <summary>
	/// /
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		/// <summary>
		/// 
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			#region  Jwt
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs")),//�����������±�
				ValidateIssuer = true,
				ValidIssuer = "Jasonbourne",//������
				ValidateAudience = true,
				ValidAudience = "HeyMacchiato",//������
				ValidateLifetime = true,
				ClockSkew = TimeSpan.FromMinutes(30),
				RequireExpirationTime = true,
			};
			var permission = new List<PermissionItem> {
							   //new PermissionItem {  Url="/", Name="Admin"},
							   //new PermissionItem {  Url="/api/values", Name="Admin"},
							   //new PermissionItem {  Url="/", Name="system"},
							   //new PermissionItem {  Url="/api/values1", Name="system"}
			};
			var permissionRequirement = new PermissionRequirement(
				"/api/denied",// �ܾ���Ȩ����ת��ַ��Ŀǰ���ã�
				permission,//Ȩ�޼���
				ClaimTypes.Role,//���ڽ�ɫ����Ȩ
				"Jasonbourne",//������
				"HeyMacchiato",//������
				 new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs")), SecurityAlgorithms.HmacSha256),//ǩ��ƾ��
				expiration: TimeSpan.FromHours(2)//�ӿڵĹ���ʱ�䣬ע������û���˻���ʱ�䣬��Ҳ�����Զ��壬���ϱߵ�TokenValidationParameters�� ClockSkew
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
			services.AddSingleton(permissionRequirement);
			#endregion
			services.AddSwagger("Login");
		
			services.Scan(scan => scan.FromAssemblies(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("HeyTom.Manage.Repository")))
			   .AddClasses(x => x.Where(y => y.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase)))
			   .AsImplementedInterfaces()
			   .WithScopedLifetime());
			services.AddControllers();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
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
			app.UseSwaggerUI("Login");
		}
	}
}
