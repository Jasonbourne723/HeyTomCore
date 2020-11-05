using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using HeyTom.Manage.Model;
using SqlSugar;
using System;
using System.Collections.Generic;

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

        public override PageResultModel<T1> GetPageResult<T1>(ListParam listParam) 
        {
            int totalCount = 0;
            List<IConditionalModel> where = GetWhere(listParam);
            string select = GetSelect(listParam);
            string sort = GetSort(listParam, ref select);
            var result = new PageResultModel<T1>();
            result.TModel = _multipleDb.Queryable<User,UserRole, Role>((a,c,b) => new object[] { JoinType.Left, a.Id == c.UserId ,JoinType.Left, b.Id == c.RoleId }).Where(where).OrderBy(sort).Select<T1>(select).ToPageList(listParam.PageIndex, listParam.PageSize, ref totalCount);
            result.RecordCount = totalCount;
            result.PageCount = (int)Math.Ceiling(((decimal)totalCount / listParam.PageSize));
            return result;
        }
    }

    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);
    }

}
