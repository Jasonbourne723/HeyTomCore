using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyTom.MyBlog.Model;

namespace HeyTom.MyBlog.Repository
{
    public class BlogCommentRepository : BaseRepository<BlogComment>, IBlogCommentRepository
    {
        public BlogCommentRepository()
        {
            _db = new BaseDbContext<BlogComment>("db_blog");
        }

    }

    public interface IBlogCommentRepository : IBaseRepository<BlogComment>
    {

    }
}
