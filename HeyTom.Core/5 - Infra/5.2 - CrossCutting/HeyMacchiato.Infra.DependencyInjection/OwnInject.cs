using AutoMapper;
using HeyMacchiato.Application.Member.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace HeyMacchiato.Infra.DependencyInjection
{
	public static class OwnInject
	{
		/// <summary>
		/// 注入 自己的项目依赖
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddOwnInject(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(VOToDTOProfile));
			services.Scan(scan => scan.FromAssemblies(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("HeyMacchiato.Infra.Data")))
				.AddClasses(x => x.Where(y => y.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase)))
				.AsImplementedInterfaces()
				.WithScopedLifetime());
			services.Scan(scan => scan.FromAssemblies(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("HeyMacchiato.Domain.Member")))
							.AddClasses(x => x.Where(y => y.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase)))
							.AsImplementedInterfaces()
							.WithScopedLifetime());
			return services.Scan(scan => scan.FromAssemblies(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("HeyMacchiato.Application.Member")))
							.AddClasses(x => x.Where(y => y.Name.EndsWith("Application", StringComparison.OrdinalIgnoreCase)))
							.AsImplementedInterfaces()
							.WithScopedLifetime());
		}
	}
}
