using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyTom.MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyTom.MyBlog.Repository
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository()
        {
            _db = new BaseDbContext<Author>("db_blog");
        }

    }

    public interface IAuthorRepository : IBaseRepository<Author>
    {
    }

   
}
