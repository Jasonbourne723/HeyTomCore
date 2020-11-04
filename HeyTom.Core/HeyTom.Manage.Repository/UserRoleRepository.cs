using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyTom.Manage.Model;

namespace HeyTom.Manage.Repository
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository()
        {
            _db = new BaseDbContext<UserRole>("db_manage");
        }


    }

    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {

    }
}
