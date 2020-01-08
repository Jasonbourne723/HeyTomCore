using HeyMacchiato.Application.QuartzTask.Service;
using HeyMachiato.Infra.Queue.RabbitMq;
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
           //var _schedulerService = new StdSchedulerFactory().GetScheduler().Result;//定义计划者
           // // 定义作业并将其绑定到HelloJob类
           // IJobDetail job = JobBuilder.Create<DailyTaskJob>()
           //     .WithIdentity($"12fd3232", "dailyTaskJob")
           //     .Build();
           // // 触发作业运行
           // ITrigger trigger = TriggerBuilder.Create()
           //     .WithIdentity($"asffddsdf", "dailyTaskTrigger")
           //     .StartNow()
           //     //.StartAt(dailyTaskDTO.JobDate)
           //     .Build();

           // _schedulerService.ScheduleJob(job, trigger);

           // _schedulerService.Start();

           // Console.ReadKey();


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
