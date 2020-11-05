using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyTom.Manage.Model;
using SqlSugar;
using System.Collections.Generic;

namespace HeyTom.Manage.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository()
        {
            _db = new BaseDbContext<Role>("db_manage");
        }

        public Role GetByUserId(int userId)
        {
            return _multipleDb.Queryable<Role, UserRole>((role, userRole) => new object[] { JoinType.Inner, role.Id == userRole.RoleId })
                  .Where((role, userRole) => userRole.UserId == userId)
                  .Select((role, userRole) => new Role {Id = role.Id,Name = role.Name,Remark = role.Remark,Type = role.Type }).First();
        }

    }

    public interface IRoleRepository : IBaseRepository<Role>
    {
        Role GetByUserId(int userId);
    }

}
