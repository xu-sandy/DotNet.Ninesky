using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Ninesky.DAL
{
    /// <summary>
    /// 上下文简单工厂
    /// 创建：15、04.21 22：54
    /// 创建：徐大帅
    /// 修改：
    /// </summary>
    public class ContextFactory
    {
        /// <summary>
        /// 获取当前数据的上下文
        /// </summary>
        /// <returns></returns>
        public static NineskyDbContext GetCurrentContext() {
            //找到与执行代码一起发送的属性集
            //这里是先在CallContext中获取NineskyContext，如果为空则初始化一个NineskyContext，
            //如果存在则直接返回。看CallContext，MSDN中讲CallContext提供对每个逻辑执行线程都唯一的数据槽，
            //而在WEB程序里，每一个请求恰巧就是一个逻辑线程所以可以使用CallContext来实现单个请求之内的DbContext单例。
            NineskyDbContext _nContext = CallContext.GetData("NineskyContext") as NineskyDbContext;
            if (_nContext == null)
            {
                //执行代码不包含nineskycontext 实例一个新的nineskydbcontext
                //并存储给对象
                _nContext = new NineskyDbContext();
                CallContext.SetData("NineskyContext", _nContext);
            }
            return _nContext;
        }
    }
}
