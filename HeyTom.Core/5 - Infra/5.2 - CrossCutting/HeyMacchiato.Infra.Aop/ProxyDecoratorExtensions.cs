using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace HeyMacchiato.Infra.Aop
{
    public static class ProxyDecoratorExtensions
    {

        public static IServiceCollection AddScopeByProxy<TInterface, TImpl>(this IServiceCollection services) where TInterface : class where TImpl : class, TInterface, new()
        {
            services.AddScoped<TImpl>();
            services.TryAddScoped<ProxyDecorator>();

            services.AddScoped<TInterface>(serviceProvider =>
            {
                var impl = serviceProvider.GetService<TImpl>();
                var tService = new ProxyDecorator().Create<TInterface>(impl);
                return (TInterface)tService;
            });

            return services;
        }

        public static IServiceCollection AddScopeByProxy<TInterface, TImpl>(this IServiceCollection services, Action<ConfigureOption> configureOption) where TInterface : class where TImpl : class, TInterface, new()
        {
            services.AddScoped<TImpl>();
            services.TryAddScoped<ProxyDecorator>();

            services.AddScoped<TInterface>(serviceProvider =>
            {
                var impl = serviceProvider.GetService<TImpl>();
                var tService = new ProxyDecorator().Create<TInterface>(impl);
                var option = new ConfigureOption();
                configureOption(option);
                ((ProxyDecorator)tService).SetConfigureOption(option);
                return (TInterface)tService;
            });

            return services;
        }
    }
}
