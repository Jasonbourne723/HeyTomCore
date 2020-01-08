using Quartz;
using Quartz.Impl;
using System;

namespace HeyMacchiato.Infra.Quartz1
{
	public static class Test
	{
		static IScheduler sched = new StdSchedulerFactory().GetScheduler().Result;//定义计划者
		public static void Start()
		{
			//JobDetailImpl jbBossReport = new JobDetailImpl("JobTest", typeof(TestJob));//时间到来时，执行TestJob类中的context方法。同时给该执行任务命名为jdTest

			//var triggerBossReport = CronScheduleBuilder.CronSchedule("0/7 * * * * ?").Build(); //确定触发器

			//triggerBossReport.Key = new TriggerKey("triggerJobTest");//触发器命名为triggerJobTest
			//sched.ScheduleJob(jbBossReport, triggerBossReport);//计划者sched在触发器triggerBossReport执行的时候，进行jbBossReport工作。
	        sched.Start();
		}

		public static void add(DateTime datetime)
		{
			//JobDetailImpl jbBossReport = new JobDetailImpl("JobTest1", typeof(TestJob2));//时间到来时，执行TestJob类中的context方法。同时给该执行任务命名为jdTest

			//// 触发作业运行
			//ITrigger trigger = TriggerBuilder.Create()
			//	.WithIdentity($"asfsa", "Test")
			//	.StartAt(datetime)
			//	.Build();

			//sched.ScheduleJob(jbBossReport, trigger);//计划者sched在触发器triggerBossReport执行的时候，进行jbBossReport工作。
			
		}
	}
}
