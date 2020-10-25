using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using HeyTom.MyBlog.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HeyTom.MyBlog.Respository
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository()
        {
            _db = new BaseDbContext<Blog>("db_blog");
        }

       
    }

    public interface IBlogRepository : IBaseRepository<Blog>
    {
       // PageResultModel<Blog> GetPageResult(ListParam listParam);
    }
}
