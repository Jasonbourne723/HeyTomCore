using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using HeyTom.Manage.Model;

namespace HeyTom.Manage.Repository
{
    public class RoleMenuRepository : BaseRepository<RoleMenu>, IRoleMenuRepository
    {
        public RoleMenuRepository()
        {
            _db = new BaseDbContext<RoleMenu>("db_manage");
        }

        public ResultModel DeleteByRoleId(int roleId)
        {
            return new ResultModel(_currentDb.AsDeleteable().Where(x => x.RoleId == roleId).ExecuteCommand());
        }
    }

    public interface IRoleMenuRepository : IBaseRepository<RoleMenu>
    {
        ResultModel DeleteByRoleId(int roleId);

    }


   
}
