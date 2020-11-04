using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace HeyMacchiato.Service.Manage.Apps
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwagger("Manage");
            services.Scan(scan => scan.FromAssemblies(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("HeyTom.Manage.Repository")))
                .AddClasses(x => x.Where(y => y.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
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
            var permission = new List<PermissionItem>
            {
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
            services.AddControllers(option => { option.Filters.Add(new AuthorizeFilter()); });
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

            app.UseSwaggerUI("Manage");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
