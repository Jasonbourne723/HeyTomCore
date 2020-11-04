using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using HeyTom.MyBlog.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HeyTom.MyBlog.Repository
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository()
        {
            _db = new BaseDbContext<Blog>("db_blog");
        }

        public ResultModel Remove(int id)
        {
            return new ResultModel(_multipleDb.Updateable<Blog>().SetColumns(x => new Blog() { IsDel = 1 }).Where(it => it.Id == id).ExecuteCommand());
        }
    }

    public interface IBlogRepository : IBaseRepository<Blog>
    {
        ResultModel Remove(int id);
    }
}
