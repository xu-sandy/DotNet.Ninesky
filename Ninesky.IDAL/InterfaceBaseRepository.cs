using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Ninesky.IDAL
{
    /// <summary>
    /// 接口基类
    /// 创建： 徐大帅
    /// 创建： 15/04/21 22:18
    /// 修改
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public interface InterfaceBaseRepository<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>添加后的数据实体</returns>
        T Add(T entity);
        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns>记录数</returns>
        int Count(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是否成功</returns>
        bool Update(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是否成功</returns>
        bool Delete(T entity);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="anyLambda">查询表达式</param>
        /// <returns>布尔值</returns>
        bool Exist(Expression<Func<T, bool>> anyLambda);
        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns>实体</returns>
        T Find(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="orderName"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<T> FindList(Expression<Func<T, bool>> whereLambda, string orderName, bool isAsc);
        /// <summary>
        /// 查询分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderName"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLambda, string orderName, bool isAsc);
        //貌似不太好用
        ///// <summary>
        ///// 查询数据列表
        ///// </summary>
        ///// <typeparam name="S">排序</typeparam>
        ///// <param name="whereLambda">条件表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <param name="orderLambda">排序表达式</param>
        ///// <returns></returns>
        //IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderLambda);

        /// <summary>
        /// 查询分页数据列表
        /// </summary>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <returns></returns>
        //IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderLambda);
    }
}
