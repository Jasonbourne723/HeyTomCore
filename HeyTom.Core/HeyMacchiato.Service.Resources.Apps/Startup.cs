using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeyMacchiato.Infra.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using HeyMacchiato.Infra.MvcCore;
using System.Runtime.Loader;
using System.Reflection;

namespace HeyMacchiato.Service.Resources.Apps
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
            services.AddSwagger("Resources");
            services.Scan(scan => scan.FromAssemblies(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("HeyTom.Resources.Repository")))
                .AddClasses(x => x.Where(y => y.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
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
            services.AddControllers(option => {
                option.Filters.Add(new JwtAuthorziationActionAttribute(tokenValidationParameters));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //    app.UseAuthorization();

            app.UseSwaggerUI("Resources");

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            }); // For the wwwroot folder

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
