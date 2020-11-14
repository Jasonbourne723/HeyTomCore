using HeyMacchiato.Application.QuartzTask.Service;
using HeyMachiato.Infra.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Configuration;
using System.IO;
using Topshelf;

namespace HeyMacchiato.Service.QuartzTask
{
	class Program
	{
		static void Main(string[] args)
		{

            var services = new ServiceCollection();
            services.AddSingleton<SchedulerService>();
            services.AddScoped<DailyTaskJob>();
            services.AddSingleton<IRabbitMqService, RabbitMqService>();
            services.AddScoped<QuartzService>();
            services.AddScoped(typeof(ServiceRunner));
            var serviceProvider = services.BuildServiceProvider();
            HostFactory.Run(x => {
                x.Service<ServiceRunner>(s => {
                    s.ConstructUsing(name => serviceProvider.GetService<ServiceRunner>());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                }
                    );
                x.RunAsLocalSystem();
                x.EnablePauseAndContinue();
                x.SetDescription("QuartzHeyMacchiato");        //安装服务后，服务的描述
                x.SetDisplayName("QuartzHeyMacchiato");                       //显示名称
                x.SetServiceName("QuartzHeyMacchiato");                       //服务名称
            });
            Console.ReadLine();
        }
	}
}
