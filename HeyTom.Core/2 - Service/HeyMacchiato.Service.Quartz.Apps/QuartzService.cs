using HeyMacchiato.Application.QuartzTask.DTO;
using HeyMacchiato.Application.QuartzTask.Service;
using HeyMachiato.Infra.Queue.RabbitMq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Service.QuartzTask
{
	public class QuartzService
	{
		private readonly IRabbitMqService _rabbitMqService;
		private readonly SchedulerService _schedulerService;
		private readonly DailyTaskJob _dailyTaskJob;

		public QuartzService(IRabbitMqService rabbitMqService,
									SchedulerService schedulerService,
									DailyTaskJob dailyTaskJob)
		{
			this._rabbitMqService = rabbitMqService;
			this._schedulerService = schedulerService;
			this._dailyTaskJob = dailyTaskJob;
		}

		public void StartConsume()
		{
			_rabbitMqService.Subscribe<DailyTaskDTO>(ea => {
				_dailyTaskJob.AddJob(ea);
			});
		}

		public void Start()
		{
			_schedulerService.Start();
		}
	}
}
