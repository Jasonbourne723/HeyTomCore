using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Domain.Member.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Member.Repository
{
	public interface IPostRepository : IBaseRepository<Post>
	{
		List<Post> GetByMemberId(int memberId);
	}
}
