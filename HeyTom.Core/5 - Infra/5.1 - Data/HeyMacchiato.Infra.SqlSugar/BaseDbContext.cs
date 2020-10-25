using HeyMacchiato.Domain.Core.Models;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HeyMacchiato.Infra.SqlSugar
{
	public class BaseDbContext<T> where T : class,new()
	{
		public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
		public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来处理T表的常用操作
		
		public BaseDbContext(string db)
		{
			var configuration = JsonConfigurationExtensions.AddJsonFile(new ConfigurationBuilder(), "appsettings.json").Build();
			Db = new SqlSugarClient(new ConnectionConfig()
			{
				ConnectionString = configuration[db],
				DbType = DbType.MySql,
				InitKeyType = InitKeyType.SystemTable,//从特性读取主键和自增列信息
				IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
				IsShardSameThread = true
			});
		}

		public virtual List<T> GetViewPage(Expression<Func<T, bool>> where ,int pageIndex,int pageSize,ref  int totalCount)
		{
			return CurrentDb.AsQueryable().Where(where).ToPageList(pageIndex, pageSize, ref totalCount);
		}

		/// <summary>
		/// 获取所有
		/// </summary>
		/// <returns></returns>
		public virtual List<T> GetList(int count)
		{
			return CurrentDb.AsQueryable().Take(count)?.ToList();
		}
		/// <summary>
		/// 通过主键获取实体
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual T GetById(long id)
		{
			return CurrentDb.GetById(id);
		}
		/// <summary>
		/// 插入
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual int Insert(T entity)
		{
			return CurrentDb.AsInsertable(entity).ExecuteCommandIdentityIntoEntity()? 1:0;
		}
		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public virtual int InsertBulk(List<T> entities)
		{
			return CurrentDb.InsertRange(entities)?1:0;
		}
		/// <summary>
		/// 根据主键删除
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual int Delete(dynamic id)
		{
			return CurrentDb.Delete(id)?1:0;
		}
		/// <summary>
		/// 批量删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public virtual int Deletes(List<dynamic> ids)
		{
			return CurrentDb.DeleteByIds(ids.ToArray())?1:0;
		}
		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual int Update(T entity)
		{
			return CurrentDb.Update(entity)?1:0;
		}
		/// <summary>
		/// 开启事务
		/// </summary>
		public void BeginTran()
		{
			Db.Ado.BeginTran();
		}
		/// <summary>
		/// 提交事务
		/// </summary>
		public void CommitTran()
		{
			Db.Ado.CommitTran();
		}
		/// <summary>
		/// 事务回滚
		/// </summary>
		public void RollBack()
		{
			Db.Ado.RollbackTran();
		}
	}
}
