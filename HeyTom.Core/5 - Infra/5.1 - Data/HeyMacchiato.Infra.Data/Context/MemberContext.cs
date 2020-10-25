using HeyMacchiato.Domain.Member.Models;
using HeyMacchiato.Infra.SqlSugar;
using SqlSugar;
using System;

namespace HeyMacchiato.Infra.Data
{
    public class MemberContext : BaseDbContext<Member>
    {
        public MemberContext(string db) : base(db)
        {
        }
    }
}
