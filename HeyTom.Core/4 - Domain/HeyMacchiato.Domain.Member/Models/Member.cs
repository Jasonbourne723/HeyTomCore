using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Member.Models
{
	public class Member
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string NickName { get; set; }

		public short Sex { get; set; }

		public DateTime Birthday { get; set; }

		public string Email { get; set; }

		public string WxOpenId { get; set; }

		public string GitHubId { get; set; }

		public string Password { get; set; }

		public int GradeId { get; set; }

		public int Point { get; set; }
	}
}
