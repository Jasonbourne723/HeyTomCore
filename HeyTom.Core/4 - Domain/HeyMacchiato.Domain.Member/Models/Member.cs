using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Member.Models
{
	public class Member
	{
		public Member()
		{ 
		}

		public Member(string nickName, string gitHubId)
		{
			NickName = nickName;
			GitHubId = gitHubId;
		}

		public Member(string nickName, string email, string password)
		{
			NickName = nickName;
			Email = email;
			Password = password;
		}

		public int Id { get; private set; }

		public string Name { get; private set; }

		public string NickName { get; private set; }

		public short Sex { get; private set; }

		public DateTime Birthday { get; private set; }

		public string Email { get; private set; }

		public string WxOpenId { get; private set; }

		public string GitHubId { get; private set; }

		public string Password { get; private set; }

		public int GradeId { get; private set; }

		public int Point { get; private set; }

	}
}
