using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HeyMacchiato.Infra.MvcCore
{
	public static class SwaggerDependencyInjection
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services, string title)
		{
			//注册Swagger生成器，定义一个和多个Swagger 文档
			return services.AddSwaggerGen(c =>
			{
				var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.XML";
				var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, $"{xmlFile}");
				c.IncludeXmlComments(xmlPath);
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = title,
					Version = "V1",
				});
			});
		}

		public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, string title)
		{
			app.UseSwagger();
			return app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("../swagger/v1/swagger.json", title);
			});
		}
	}
}
