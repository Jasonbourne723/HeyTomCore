using HeyMacchiato.Domain.Core.Models;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Member.Models
{
	public class Member : Entity
	{
		public Member()
		{ 
		}

		/// <summary>
		/// github注册
		/// </summary>
		/// <param name="nickName"></param>
		/// <param name="gitHubId"></param>
		public Member(string nickName, string gitHubId)
		{
			NickName = nickName;
			GitHubId = gitHubId;
			Email = "";
			Password = "";
			Name = "";
			Sex = 0;
			Birthday = DateTime.MinValue;
			WxOpenId = "";
			GradeId = 0;
			Point = 0;
		}

		/// <summary>
		/// 邮箱注册
		/// </summary>
		/// <param name="nickName"></param>
		/// <param name="email"></param>
		/// <param name="password"></param>
		public Member(string nickName, string email, string password)
		{
			NickName = nickName;
			Email = email;
			Password = password;
			Name = "";
			Sex = 0;
			Birthday = DateTime.MinValue;
			WxOpenId = "";
			GitHubId = "";
			GradeId = 0;
			Point = 0;
		}

		public string Name { get;  set; }

		public string NickName { get;  set; }

		public short Sex { get;  set; }

		public DateTime Birthday { get;  set; }

		public string Email { get;  set; }

		public string WxOpenId { get;  set; }

		public string GitHubId { get;  set; }

		public string Password { get;  set; }

		public int GradeId { get;  set; }

		public int Point { get;  set; }

		/// <summary>
		/// 更新个人信息
		/// </summary>
		/// <param name="name"></param>
		/// <param name="nickName"></param>
		/// <param name="sex"></param>
		/// <param name="birthday"></param>
		public void UpdateInfo(string name, string nickName, short sex, DateTime birthday) 
		{
			Name = name;
			NickName = nickName;
			Sex = sex;
			Birthday = birthday;
		}

		/// <summary>
		/// 发布动态
		/// </summary>
		/// <returns></returns>
		public Post Post(string title,string content,short type,string image1=null,string image2=null,string image3=null)
		{
			return new Post(this.Id, title, content, type, image1, image2, image3);
		}
	}
}
