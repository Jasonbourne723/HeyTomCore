using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyTom.MyBlog.Model;
using System.Collections.Generic;

namespace HeyTom.MyBlog.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository()
        {
            _db = new BaseDbContext<Category>("db_blog");
        }

        public List<Category> GetInUse()
        { 
            return _db.CurrentDb.GetList(x=>x.IsDel ==0);
        }

        public List<Category> GetAll()
        {
            return _db.CurrentDb.GetList();
        }
    }

    public interface ICategoryRepository : IBaseRepository<Category>
    {

        List<Category> GetInUse();

        List<Category> GetAll();

    }
}
