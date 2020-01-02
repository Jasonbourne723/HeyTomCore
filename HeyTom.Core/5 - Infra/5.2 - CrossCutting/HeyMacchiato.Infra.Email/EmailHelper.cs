using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace HeyMacchiato.Infra.Email
{
	public static class EmailHelper
	{
		public static ResultModel Send(List<string> toEmail, string subject, string content, string fromName, string userName = null, string host = "smtp.qq.com", string fromEmail = "420994592@qq.com", string passWord = "tcjiawjzjfodbifd")
		{
			var result = new ResultModel(1, "发送成功");
			SmtpClient smtpClient = new SmtpClient
			{
				Host = host,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromEmail, passWord),
				EnableSsl = true
			};
			try
			{
				MailAddress from = new MailAddress(fromEmail);
				var mailMessage = new MailMessage
				{
					From = from,
					Subject = subject,
					Body = content,
					BodyEncoding = Encoding.Default,
					IsBodyHtml = true,
					Priority = MailPriority.Normal,
				};
				toEmail?.ForEach(ea => {
					mailMessage.To.Add(ea);
				});
				smtpClient.Send(mailMessage);
			}
			catch (Exception ex)
			{
				result.ResultNo = -1;
				result.Message = ex.Message;
			}
			return result;
		}
	}
}
