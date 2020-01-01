using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HeyMacchiato.Infra.Dapper
{
	public  class DataContext
	{
		private readonly static string _connectionString = "Server=gz-cynosdbmysql-grp-lej3steh.sql.tencentcdb.com;Port=22877;Database=HeyMacchiato;Uid=root;Pwd=Gaohan521;SslMode=none;";

		public  int Execute(string sql,object param)
		{
			using (IDbConnection cnn = new MySqlConnection(_connectionString))
			{
				return cnn.Execute(sql, param);
			}
		}

		public  List<T> QueryList<T>(string sql, object param)
		{
			using (IDbConnection cnn = new SqlConnection(_connectionString))
			{
				return cnn.Query<T>(sql, param)?.ToList();
			}
		}
	}
}
