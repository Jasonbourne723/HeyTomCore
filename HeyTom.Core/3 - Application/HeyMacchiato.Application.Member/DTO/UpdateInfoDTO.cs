using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Application.Member.DTO
{
 	public class UpdateInfoDTO
	{
		public int Id { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 昵称
		/// </summary>
		public string NickName { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public short Sex { get; set; }
		/// <summary>
		/// 生日
		/// </summary>
		public DateTime Birthday { get; set; }
	}
}
