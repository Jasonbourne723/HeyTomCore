using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HeyMacchiato.Application.Member.DTO
{
	public class CreatePostDTO
	{
		[Required]
		public int MemberId { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		[Required]
		public string Title { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		[Required]
		public string Content { get; set; }
		/// <summary>
		/// 类型 0：动态 1：领养
		/// </summary>
		[Required]
		public short Type { get; set; }

		public string Image1 { get; set; }

		public string Image2 { get; set; }

		public string Image3 { get; set; }
	}
}
