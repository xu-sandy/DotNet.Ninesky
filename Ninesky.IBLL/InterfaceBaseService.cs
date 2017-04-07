using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.IBLL
{
    /// <summary>
    /// 接口基类
    /// 创建：15.04.22 22：22
    /// 创建：徐大帅
    /// 修改：
    /// </summary>
    public interface InterfaceBaseService<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>添加后的实体</returns>
        T Add(T entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns>是否成功</returns>
        bool Update(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是否成功</returns>
        bool Delete(T entity);
    }
}
