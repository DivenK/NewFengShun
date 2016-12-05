using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.Repository
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq.Expressions;
    using itcast.crm16.IRepository;
    using System.Runtime.Remoting.Messaging;

    public class BaseDal<TEntity> : IBaseDal<TEntity> where TEntity : class
    {
        //1.0 实例化EF上下文容器     
        //缺陷：如果在一个控制器中有多个services的对象，就会存在多个ef上下文容器
        //就会存在一个action会打开关闭多次ado.net连接,同时还需要调用每个services的savechanges方法才能正常操作表数据
        //BaseDbContext db = new BaseDbContext();

        //改进上述缺陷：目的是保证一个线程不管有几个services对象只有一个EF上下文容器
        public BaseDbContext db
        {
            get
            {
                string key = typeof(BaseDbContext).FullName;

                //从线程缓存中获取
                object dbobj = CallContext.GetData(key);

                //判断如果dbobj为null则要实例化BaseDbContext 
                if (dbobj == null)
                {
                    //初始化
                    dbobj = new BaseDbContext();

                    //将dbobj存入线程缓存中
                    CallContext.SetData(key, dbobj);
                }

                return dbobj as BaseDbContext;
            }
        }

        //2.0 
        DbSet<TEntity> _dbset;

        public BaseDal()
        {
            _dbset = db.Set<TEntity>();
        }

        #region 3.0 查询

        #region 3.0.1 普通带条件查询

        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }


        #endregion

        #region 3.0.2 带条件的连表查询

        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            if (tableNames == null || tableNames.Any() == false)
            {
                throw new Exception("连表方法的tableNames参数至少有一个");
            }

            //2.0 连表操作
            DbQuery<TEntity> query = _dbset;
            foreach (var tablename in tableNames)
            {
                query = query.Include(tablename);
            }

            return query.Where(where).ToList();
        }

        #endregion

        #region 3.0.3 带条件的分页查询

        public List<TEntity> QueryByPage<TKey>(int pageindex
            , int pagesize
            , out int rowcount
            , Expression<Func<TEntity, bool>> where
            , Expression<Func<TEntity, TKey>> order)
        {
            //1.0 计算要跳过的总条数
            int skipCount = (pageindex - 1) * pagesize;

            //2.0 获取总条数
            rowcount = _dbset.Count(where);

            //3.0 开始分页：注意先排序
            return _dbset.Where(where).OrderByDescending(order).Skip(skipCount).Take(pagesize).ToList();
        }

        #endregion

        #region 3.0.4 通用调用存储过程的方法

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="TElement">是在调用的时候传入的一个实体，此实体必须和调用的存储过程返回结果集中的字段名称保持一致</typeparam>
        /// <param name="sql">如果调用的是存储过程 写法样例: Usp_GetMneus @uid ,Usp_GetMneus 1</param>
        /// <param name="prms">参数化查询的参数数组</param>
        /// <returns></returns>
        public List<TElement> RunProc<TElement>(string sql, params object[] prms)
        {
            return db.Database.SqlQuery<TElement>(sql, prms).ToList();
        }

        #endregion

        #endregion

        #region 4.0 新增

        public void Add(TEntity model)
        {
            //1.0 
            if (model == null)
            {
                throw new Exception("add中的参数model不能为null");
            }

            //2.0 调用add方法将model追加到ef上下文容器中同时修改其状态为added
            _dbset.Add(model);
        }

        #endregion


        #region 5.0 编辑

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">没有被ef容器管理的</param>
        /// <param name="propertyNames"></param>
        public void Edit(TEntity model, string[] propertyNames)
        {
            //1.0 合法性验证
            if (model == null)
            {
                throw new Exception("Edit中的参数model不能为null");
            }

            if (propertyNames == null || propertyNames.Any() == false)
            {
                throw new Exception("Edit中的参数propertyNames至少要有一个属性");
            }

            //2.0 将model追加到EF容器中
            System.Data.Entity.Infrastructure.DbEntityEntry entry = db.Entry(model);
            entry.State = System.Data.EntityState.Unchanged;
            foreach (var item in propertyNames)
            {
                entry.Property(item).IsModified = true;
            }

            //3.0 关闭EF的实体属性合法性验证
            db.Configuration.ValidateOnSaveEnabled = false;
        }

        #endregion

        #region 6.0 物理删除

        public void Delete(TEntity model, bool isaddedContext)
        {
            //1.0
            if (model == null)
            {
                throw new Exception("Delete中的参数model不能为null");
            }

            //2.0 
            if (isaddedContext == false)
            {
                _dbset.Attach(model);
            }

            _dbset.Remove(model);
        }

        #endregion

        #region 7.0 统一保存

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        #endregion

    }
}
