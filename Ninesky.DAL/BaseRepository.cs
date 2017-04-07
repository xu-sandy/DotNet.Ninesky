using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.IDAL;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Ninesky.DAL
{
    /// <summary>
    /// 存储基类
    /// 创建：15、21、04 22：48
    /// 创建：徐大帅
    /// 修改：
    /// 创建基类 并且继承InterfaceBaseRepository 实现其方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : InterfaceBaseRepository<T> where T : class
    {
        protected NineskyDbContext nContext = ContextFactory.GetCurrentContext();
        /// <summary>
        /// 添加接口方法的实现
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            nContext.Entry<T>(entity).State = EntityState.Added;
            nContext.SaveChanges();
            return entity;
        }
        /// <summary>
        /// 查询记录数接口方法实现
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return nContext.Set<T>().Count(predicate);
        }

        /// <summary>
        /// 更新接口方法的实现
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            nContext.Set<T>().Attach(entity);
            nContext.Entry<T>(entity).State = EntityState.Modified;
            return nContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除接口方法的实现
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            nContext.Set<T>().Attach(entity);
            nContext.Entry<T>(entity).State = EntityState.Deleted;
            return nContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 查询是否存在的接口方法的实现
        /// </summary>
        /// <param name="anyLambda"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return nContext.Set<T>().Any(anyLambda);
        }
        /// <summary>
        /// 查找单个实体的接口方法实现
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = nContext.Set<T>().SingleOrDefault<T>(whereLambda);
            return _entity;
        }
        /// <summary>
        /// 查询实体集合接口方法实现
        /// </summary>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="isAsc">是否升序</param>
       
        /// <returns>Iqueryable</returns>
        public IQueryable<T> FindList(Expression<Func<T,bool>> whereLambda,string orderName,bool isAsc)
        {
            var _list = nContext.Set<T>().Where(whereLambda);
            _list = OrderBy(_list, orderName, isAsc);
            return _list;

            //var _list = nContext.Set<T>().Where<T>(whereLambda);
            //if (isAsc)
            //    _list = _list.OrderBy<T, S>(orderLambda);
            //else
            //    _list = _list.OrderByDescending<T, S>(orderLambda);
            //return _list;
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderName"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLambda,string orderName,bool isAsc)
        {
            var _list = nContext.Set<T>().Where<T>(whereLambda);
            totalRecord = _list.Count();
            _list = OrderBy(_list, orderName, isAsc).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return _list;
        }

        ///// <summary>
        ///// 查询分页数据列表接口方法实现
        ///// </summary>
        ///// <typeparam name="S">排序</typeparam>
        ///// <param name="pageIndex">当前页</param>
        ///// <param name="pageSize">页大小</param>
        ///// <param name="totalRecord">总记录数</param>
        ///// <param name="whereLambda">查询表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <param name="orderLambda">排序表达式</param>
        ///// <returns></returns>
        //public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> wherelambda, bool isAsc, Expression<Func<T, S>> orderLambda)
        //{
        //    var _list = nContext.Set<T>().Where<T>(wherelambda);
        //    totalRecord = _list.Count();
        //    if (isAsc)
        //        _list = _list.OrderBy<T, S>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
        //    else
        //        _list = _list.OrderByDescending<T, S>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
        //    return _list;
        //}
        /// <summary>
        /// 排序方法
        /// </summary>
        /// <param name="source">原Iqueryable</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="isAsc"></param>
        /// <returns>返回排序后的Iqueryable</returns>
        public IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc)
        {
            if (source == null)
            {
                throw new ArgumentException("source", "不能为空");
            }
            if (string.IsNullOrEmpty(propertyName))
            {
                return source;
            }
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter, propertyName);
            if (_property == null)
            {
                throw new ArgumentException("propertyName", "属性不存在");
            }
            var _lambda = Expression.Lambda(_property, _parameter);
            var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof(Queryable), _methodName, new Type[] { source.ElementType, _property.Type }, source.Expression, Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);
        }
    }
}
