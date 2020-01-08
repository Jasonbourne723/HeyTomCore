using HeyMacchiato.Application.QuartzTask.DTO;
using HeyMacchiato.Infra.Email;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeyMacchiato.Application.QuartzTask.Service
{
	public class DailyTaskJob : IJob
	{
		private  SchedulerService _schedulerService;

		public DailyTaskJob(SchedulerService schedulerService)
		{
			_schedulerService = schedulerService;
		}

		public DailyTaskJob()
		{ 
		}

		public Task Execute(IJobExecutionContext context)
		{
			var data = context.JobDetail.JobDataMap;
			EmailHelper.Send(data["Email"].ToString(), data["Subject"].ToString(), data["Content"].ToString());
			return Task.CompletedTask;
		}

		public  void AddJob(DailyTaskDTO dailyTaskDTO)
		{
			var dic = new Dictionary<string, string>() {
					{ "Email", dailyTaskDTO.Email },
					{ "Subject",dailyTaskDTO.Subject},
					{ "Content",dailyTaskDTO.Content}
				};
			var map = new JobDataMap(dic);

			// 定义作业并将其绑定到HelloJob类
			IJobDetail job = JobBuilder.Create<DailyTaskJob>()
				.WithIdentity($"Task{dailyTaskDTO.TaskId}", "dailyTaskJob")
				.SetJobData(map)
				.Build();
			// 触发作业运行
			ITrigger trigger = TriggerBuilder.Create()
				.WithIdentity($"Task{dailyTaskDTO.TaskId}", "dailyTaskTrigger")
				//.StartNow()
				.StartAt(dailyTaskDTO.JobDate)
				.Build();

			_schedulerService.AddJob(job, trigger);
		}

	}
}
