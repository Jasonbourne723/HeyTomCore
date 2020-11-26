using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeyMacchiato.service.MessageNotity.Apps.BgService;
using HeyMacchiato.service.MessageNotity.Apps.Controllers;
using HeyMachiato.Infra.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HeyMacchiato.service.MessageNotity.Apps
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

            services.AddSignalR();

            services.AddCors(options => {
                options.AddPolicy("SignalRCors", policy => policy.AllowAnyOrigin()
                                                                 .AllowAnyHeader()
                                                                 .AllowAnyMethod()
                                                            );

            });
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs")),//参数配置在下边
                ValidateIssuer = true,
                ValidIssuer = "Jasonbourne",//发行人
                ValidateAudience = true,
                ValidAudience = "HeyMacchiato",//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(30),
                RequireExpirationTime = true,
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddHttpContextAccessor();
            services.AddSingleton<IRabbitMqService, RabbitMqService>();
            services.AddControllers();
            services.AddHostedService<SubscribeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("SignalRCors");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CommentHub>("/CommentHub");
            });
        }
    }
}
