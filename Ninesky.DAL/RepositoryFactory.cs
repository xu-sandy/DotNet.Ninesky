using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.IDAL;

namespace Ninesky.DAL
{
    /// <summary>
    /// 创建简单工厂
    /// 用来返回项目中的所有Repository类
    /// </summary>
    public static class RepositoryFactory
    {
        /// <summary>
        /// 用户仓储
        /// 在bll调用的时候就不用每次 InterfaceUserRepository _iUserRsy = new  UserRepository()
        /// 直接写成  InterfaceUserRepository _iUserRsy = RepositoryFactory.UserRepository
        /// 好处就是，以后在DAL项目中实现InterfaceUserRepository接口的类需要修改时我们可以直接创建个新类，
        /// 然后RepositoryFactory类中让UserRepository属性返回新类就行了
        /// </summary>
        public static InterfaceUserRepository UserRepository { get { return new UserRepository(); } }
    }
}
