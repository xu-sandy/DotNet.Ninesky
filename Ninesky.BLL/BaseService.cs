using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.IBLL;
using Ninesky.IDAL;

namespace Ninesky.BLL
{
    /// <summary>
    /// 服务基类
    /// 这个类的构造函数中要传入一个参数就是currentRepository 这个在继承的时候进行传入
    /// 创建：15.04.22 22：35
    /// 创建：徐大帅
    /// 修改：
    /// </summary>
    public abstract class BaseService<T> : InterfaceBaseService<T> where T : class
    {
        protected InterfaceBaseRepository<T> CurrentRepository { get; set; }
        public BaseService(InterfaceBaseRepository<T> currentRepository)
        {
            CurrentRepository = currentRepository;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            return CurrentRepository.Add(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            return CurrentRepository.Update(entity);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            return CurrentRepository.Delete(entity);
        }
    }
}
