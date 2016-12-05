using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace itcast.crm16.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">一定是EF T4模板生成的实体类</typeparam>
    public interface IBaseDal<TEntity> where TEntity : class
    {
        #region 3.0 查询

        #region 3.0.1 普通带条件查询

        List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where);
      


        #endregion

        #region 3.0.2 带条件的连表查询

        List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames);
      

        #endregion

        #region 3.0.3 带条件的分页查询

        List<TEntity> QueryByPage<TKey>(int pageindex
           , int pagesize
           , out int rowcount
           , Expression<Func<TEntity, bool>> where
           , Expression<Func<TEntity, TKey>> order);
      

        #endregion

        #region 3.0.4 通用调用存储过程的方法

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="TElement">是在调用的时候传入的一个实体，此实体必须和调用的存储过程返回结果集中的字段名称保持一致</typeparam>
        /// <param name="sql">如果调用的是存储过程 写法样例: Usp_GetMneus @uid ,Usp_GetMneus 1</param>
        /// <param name="prms">参数化查询的参数数组</param>
        /// <returns></returns>
        List<TElement> RunProc<TElement>(string sql, params object[] prms);
       

        #endregion

        #endregion

        #region 4.0 新增

        void Add(TEntity model);
      

        #endregion


        #region 5.0 编辑

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">没有被ef容器管理的</param>
        /// <param name="propertyNames"></param>
        void Edit(TEntity model, string[] propertyNames);
      

        #endregion

        #region 6.0 物理删除

        void Delete(TEntity model, bool isaddedContext);     

        #endregion

        #region 7.0 统一保存

        int SaveChanges();
     
        #endregion
    }
}
