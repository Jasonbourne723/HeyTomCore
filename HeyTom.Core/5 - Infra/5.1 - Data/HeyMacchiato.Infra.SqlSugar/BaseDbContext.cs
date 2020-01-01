using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Infra.SqlSugar
{
	public class BaseDbContext<T> where T : class, new()
	{
		private SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
		public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来处理T表的常用操作
		public BaseDbContext()
		{
			Db = new SqlSugarClient(new ConnectionConfig()
			{
				ConnectionString = "Server=gz-cynosdbmysql-grp-lej3steh.sql.tencentcdb.com;Port=22877;Database=HeyMacchiato;Uid=root;Pwd=Gaohan521;SslMode=none;",
				DbType = DbType.MySql,
				InitKeyType = InitKeyType.SystemTable,//从特性读取主键和自增列信息
				IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
			});
		}

		/// <summary>
		/// 获取所有
		/// </summary>
		/// <returns></returns>
		public virtual List<T> GetList(int count)
		{
			return CurrentDb.AsQueryable().Take(count)?.ToList();
		}

		public virtual T GetById(long id)
		{
			return CurrentDb.GetById(id);
		}

		public virtual int Insert(T entity)
		{
			return CurrentDb.Insert(entity)?1:0;
		}

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
	}
}
