using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.DTO
{
	public class PostDTO
	{
		/// <summary>
		/// 
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
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
		public short Type { get; set; }

		public string Image1 { get; set; }

		public string Image2 { get; set; }

		public string Image3 { get; set; }
	}
}
