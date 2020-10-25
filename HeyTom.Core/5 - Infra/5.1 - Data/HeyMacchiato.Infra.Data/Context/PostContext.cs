using HeyMacchiato.Domain.Member.Models;
using HeyMacchiato.Infra.SqlSugar;

namespace HeyMacchiato.Infra.Data
{
    public class PostContext : BaseDbContext<Post>
    {
        public PostContext(string db) : base(db)
        {
        }
    }
}
