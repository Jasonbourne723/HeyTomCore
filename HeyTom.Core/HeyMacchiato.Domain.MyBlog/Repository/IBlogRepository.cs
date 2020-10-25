using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.MyBlog.Repository
{
    public interface IBlogRepository : IBaseRepository<Blog>
    {
        PageResultModel<Blog> GetPageResult(string where, int pageIndex, int pageSize);
    }
}
