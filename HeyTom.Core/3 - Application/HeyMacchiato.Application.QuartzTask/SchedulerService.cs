using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.QuartzTask
{
	public  class SchedulerService
	{
		private static IScheduler _scheduler;

		private static SchedulerService _schedulerService = new SchedulerService();

		private SchedulerService()
		{

		}

		public SchedulerService GetInstance()
		{
			return _schedulerService;
		}

		public void Start()
		{
			_scheduler = new StdSchedulerFactory().GetScheduler().Result;//定义计划者
			//2、开启调度器
			_scheduler.Start(); 
		}

		
	}
}
