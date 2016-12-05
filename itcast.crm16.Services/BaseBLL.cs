using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.Services
{
    using itcast.crm16.IServices;
    using System.Linq.Expressions;
    using itcast.crm16.IRepository;

    public class BaseBLL<TEntity> : IBaseBLL<TEntity> where TEntity : class
    {
        protected IBaseDal<TEntity> basedal;

        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
        {
            return basedal.QueryWhere(where);
        }

        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            return basedal.QueryJoin(where, tableNames);
        }

        public List<TEntity> QueryByPage<TKey>(int pageindex, int pagesize, out int rowcount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order)
        {
            return basedal.QueryByPage(pageindex, pagesize, out rowcount, where, order);
        }

        public List<TElement> RunProc<TElement>(string sql, params object[] prms)
        {
            return basedal.RunProc<TElement>(sql, prms);
        }

        public void Add(TEntity model)
        {
            basedal.Add(model);
        }

        public void Edit(TEntity model, string[] propertyNames)
        {
            basedal.Edit(model, propertyNames);
        }

        public void Delete(TEntity model, bool isaddedContext)
        {
            basedal.Delete(model, isaddedContext);
        }

        public int SaveChanges()
        {
            return basedal.SaveChanges();
        }
    }
}
