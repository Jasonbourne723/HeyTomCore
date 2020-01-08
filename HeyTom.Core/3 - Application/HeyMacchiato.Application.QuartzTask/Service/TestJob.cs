using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeyMacchiato.Application.QuartzTask.Service
{
	public class TestJob : IJob
	{
		public Task Execute(IJobExecutionContext context)
		{
			Console.WriteLine(111);
			return Task.CompletedTask;
		}
	}
}
