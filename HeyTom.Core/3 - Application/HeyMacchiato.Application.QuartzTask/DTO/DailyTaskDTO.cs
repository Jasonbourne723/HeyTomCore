using HeyMachiato.Infra.RabbitMq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.QuartzTask.DTO
{
	[RabbitMq("sendEmail","DailyTask","direct","DailyTask")]
	public class DailyTaskDTO
	{
		/// <summary>
		/// 任务Id
		/// </summary>
		public long TaskId { get; set; }
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 执行时间
		/// </summary>
		public DateTime JobDate { get; set; }
		/// <summary>
		/// 主题
		/// </summary>
		public string Subject { get; set; }
		/// <summary>
		/// 上下文
		/// </summary>
		public string Content { get; set; }
	}
}
