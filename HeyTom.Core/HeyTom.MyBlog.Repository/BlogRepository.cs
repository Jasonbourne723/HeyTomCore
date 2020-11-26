using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using HeyTom.MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
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

    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository()
        {
            _db = new BaseDbContext<Tag>("db_blog");
        }



        public ResultModel AddForBlog()
        {
            throw new NotImplementedException();
        }

        public List<BlogTagModel> GetByBlogIds(List<int> blogIds)
        {
            if (blogIds == null || blogIds.Count == 0) return null;
            return _multipleDb.Queryable<BlogTag, Tag>((a, b) => new object[] { JoinType.Left, a.TagId == b.Id }).Where((a, b) =>  blogIds.Contains(a.BlogId)).Select((a,b)=> new BlogTagModel { BlogId = a.BlogId,TagId = b.Id,TagName = b.Name })?.ToList();
             
        }

        public List<BlogTagModel> GetBlogByTagIds(List<int> tagIds)
        {
            if (tagIds == null || tagIds.Count == 0) return null;
            return _multipleDb.Queryable<BlogTag>().Where(x => tagIds.Contains(x.TagId)).Select(x=>new BlogTagModel() { BlogId = x.BlogId,TagId = x.TagId})?.ToList();

        }

    }

    public interface ITagRepository : IBaseRepository<Tag>
    {
        ResultModel AddForBlog();

        List<BlogTagModel> GetByBlogIds(List<int> blogIds);

        List<BlogTagModel> GetBlogByTagIds(List<int> tagIds);
    }
}
