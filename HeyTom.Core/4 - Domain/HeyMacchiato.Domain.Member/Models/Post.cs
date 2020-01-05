using HeyMacchiato.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Member.Models
{
	public class Post : Entity
	{
		public Post(int memberId, string title, string content, short type, string image1, string image2, string image3)
		{
			MemberId = memberId;
			Title = title;
			Content = content;
			Type = type;
			Image1 = image1;
			Image2 = image2;
			Image3 = image3;
		}

		public Post()
		{
		}

		public int MemberId { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 类型 0：动态 1：领养
		/// </summary>
		public short  Type { get; set; }

		public string Image1 { get; set; }

		public string Image2 { get; set; }

		public string Image3 { get; set; }
	}
}
