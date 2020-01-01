using HeyMacchiato.Infra.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Domain.Core.IRepository
{
	public interface IBaseRepository<T> where T : class,new()
	{
		List<T> GetList(int count);

		T GetById(long Id);

		ResultModel Insert(T entity);

		ResultModel InsertBulk(List<T> entities);

		ResultModel Update(T entity);

		ResultModel Delete(long Id);

		ResultModel Deletes(List<long> Ids);
	}
}
