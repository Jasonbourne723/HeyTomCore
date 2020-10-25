using HeyMacchiato.Domain.Member.Models;
using HeyMacchiato.Domain.Member.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Infra.Data.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
	{
		public PostRepository()
		{
			_db = new PostContext("");
		}

		public List<Post> GetByMemberId(int memberId)
		{
			return _db.CurrentDb.GetList(x => x.MemberId == memberId);
		}
	}
}
