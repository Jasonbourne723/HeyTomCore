using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Infra.Data.Repository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class,new()
	{
		protected BaseDbContext<T> _db;

		public ResultModel Delete(long id)
		{
			return new ResultModel(_db.Delete(id));
		}

		public ResultModel Deletes(List<long> ids)
		{
			return new ResultModel(_db.Deletes((dynamic)ids));
		}

		public T GetById(long id)
		{
			return _db.GetById(id);
		}

		public List<T> GetList(int count)
		{
			return _db.GetList(count);
		}

		public ResultModel Add(T entity)
		{
			return new ResultModel(_db.Insert(entity));
		}

		public ResultModel Add(List<T> entities)
		{
			return new ResultModel(_db.InsertBulk(entities));
		}

		public ResultModel Update(T entity)
		{
			return new ResultModel(_db.Update(entity));
		}
	}
}
