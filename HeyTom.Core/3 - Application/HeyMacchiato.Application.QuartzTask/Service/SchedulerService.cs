using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.QuartzTask.Service
{
	public  class SchedulerService
	{
		private  IScheduler _scheduler;

		public SchedulerService()
		{

		}

		public void Start()
		{
			_scheduler = new StdSchedulerFactory().GetScheduler().Result;//定义计划者

		}

		public async void AddJob(IJobDetail jobDetail,ITrigger trigger)
		{
			if (await _scheduler.CheckExists(jobDetail.Key)) {
				await _scheduler.DeleteJob(jobDetail.Key);
			}
			_scheduler.ScheduleJob(jobDetail, trigger);
			//2、开启调度器
			_scheduler.Start();
		}

		public void DeleteJob(JobKey jobKey)
		{
			_scheduler.DeleteJob(jobKey);
		}
	}
}
