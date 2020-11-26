using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyTom.Resources.Model;

namespace HeyTom.Resources.Repository
{
    public class PhotoCategoryRepository : BaseRepository<PhotoCategory>, IPhotoCategoryRepository
    {
        public PhotoCategoryRepository()
        {
            _db = new BaseDbContext<PhotoCategory>("db_resources");
        }

    }

    public interface IPhotoCategoryRepository : IBaseRepository<PhotoCategory>
    {

    }

 

}
