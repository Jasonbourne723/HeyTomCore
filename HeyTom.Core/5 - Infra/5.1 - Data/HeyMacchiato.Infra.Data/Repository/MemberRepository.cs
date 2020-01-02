using HeyMacchiato.Domain.Member.Models;
using HeyMacchiato.Domain.Member.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Infra.Data.Repository
{
	public class MemberRepository : BaseRepository<Member>, IMemberRepository
	{

		public MemberRepository()
		{
			_db = new MemberContext();
		}

		public Member GetByEmail(string email)
		{
			return  _db.CurrentDb.GetSingle(x => x.Email == email);
		}
	}
}
