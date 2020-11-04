using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyTom.Manage.Model;

namespace HeyTom.Manage.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository()
        {
            _db = new BaseDbContext<User>("db_manage");
        }

        public User GetByEmail(string email)
        {
            return _currentDb.GetSingle(x => x.Email == email);

        }
    }

    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);
    }

}
