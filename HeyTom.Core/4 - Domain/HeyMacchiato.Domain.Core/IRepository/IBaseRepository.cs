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

		ResultModel Add(T entity);

		ResultModel Add(List<T> entities);

		ResultModel Update(T entity);

		ResultModel Delete(long Id);

		ResultModel Deletes(List<long> Ids);

		ResultModel BeginTransaction(Action action);
	}
}
