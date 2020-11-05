using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeyMacchiato.Infra.SqlSugar
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected BaseDbContext<T> _db;

        protected SimpleClient<T> _currentDb { get { return _db.CurrentDb; } }

        protected SqlSugarClient _multipleDb { get { return _db.Db; } }

        #region 私有方法

        protected static string GetSort(ListParam listParam, ref string select)
        {
            var sort = "null";
            if (listParam.Sort != null && listParam.Sort.Count > 0)
            {
                sort = "";
                for (int i = 0; i < listParam.Sort.Count; i++)
                {
                    sort += $"{listParam.Sort[i].DbField} {listParam.Sort[i].Value}";
                    if (i != listParam.Sort.Count - 1)
                    {
                        select += ",";
                    }
                }
            }

            return sort;
        }

        protected static string GetSelect(ListParam listParam)
        {
            var select = " * ";
            if (listParam.Select != null && listParam.Select.Count > 0)
            {
                select = "";
                for (int i = 0; i < listParam.Select.Count; i++)
                {
                    select += $" {listParam.Select[i].Key} as {listParam.Select[i].Value} ";
                    if (i != listParam.Select.Count - 1)
                    {
                        select += ",";
                    }
                }
            }

            return select;
        }

        protected List<IConditionalModel> GetWhere(ListParam listParam)
        {
            var where = new List<IConditionalModel>();
            listParam.Filter?.ForEach(ea =>
            {
                where.Add(new ConditionalModel()
                {
                    FieldName = ea.DbField,
                    FieldValue = ea.Value,
                    ConditionalType = ToConditionalType(ea.Operator)

                });
            });
            return where;
        }

        protected ConditionalType ToConditionalType(string operatorStr)
        {
            operatorStr = operatorStr.Trim();
            var conditionalType = ConditionalType.Equal;
            switch (operatorStr)
            {
                case "=":
                    conditionalType = ConditionalType.Equal;
                    break;
                case ">":
                    conditionalType = ConditionalType.GreaterThan;
                    break;
                case ">=":
                    conditionalType = ConditionalType.GreaterThanOrEqual;
                    break;
                case "in":
                    conditionalType = ConditionalType.In;
                    break;
                case "<":
                    conditionalType = ConditionalType.LessThan;
                    break;
                case "<=":
                    conditionalType = ConditionalType.LessThanOrEqual;
                    break;
                case "like":
                    conditionalType = ConditionalType.Like;
                    break;
                case "!=":
                    conditionalType = ConditionalType.NoEqual;
                    break;
                default:
                    break;
            }
            return conditionalType;
        }

        #endregion 私有方法

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

        public ResultModel BeginTransaction(Action action)
        {
            var result = new ResultModel(1);
            try
            {
                _db.Db.BeginTran();
                action();
                _db.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _db.Db.RollbackTran();
                result.ResultNo = -1;
                result.Message = ex.Message;
            }
            return result;
        }

        public virtual PageResultModel<T1> GetPageResult<T1>(ListParam listParam) where T1 : class, new()
        {
            int totalCount = 0;
            List<IConditionalModel> where = GetWhere(listParam);
            string select = GetSelect(listParam);
            string sort = GetSort(listParam, ref select);
            var result = new PageResultModel<T1>();
            result.TModel = _db.CurrentDb.AsQueryable().Where(where).OrderBy(sort).Select<T1>(select).ToPageList(listParam.PageIndex, listParam.PageSize, ref totalCount);
            result.RecordCount = totalCount;
            result.PageCount = (int)Math.Ceiling(((decimal)totalCount / listParam.PageSize));
            return result;
        }

    }
}
