using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyTom.Manage.Model;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;

namespace HeyTom.Manage.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository()
        {
            _db = new BaseDbContext<Menu>("db_manage");
        }

        public List<T> GetAll<T>() where T : class, new()
        {
            return _currentDb.AsQueryable().Select<T>().ToList();
        }

        public List<T> GetByRoleId<T>(int roleId) where T : class, new()
        {
            return _multipleDb.Queryable<Menu, RoleMenu>(( menu,roleMenu) => new object[] { JoinType.Inner, roleMenu.MenuId == menu.Id})
                .Where(( menu, roleMenu) => roleMenu.RoleId == roleId).Select<T>().ToList();
        }
    }

    public interface IMenuRepository : IBaseRepository<Menu>
    {
        List<T> GetAll<T>() where T :class ,new();

        List<T> GetByRoleId<T>(int roleId) where T : class, new();
    }

}
