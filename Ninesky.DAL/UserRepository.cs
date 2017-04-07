using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.IDAL;
using Ninesky.Models;

namespace Ninesky.DAL
{
    /// <summary>
    /// 继承自BaseRepository 实现interfaceuserrepository方法
    /// 创建：15.04.22 22：10
    /// 创建：徐大帅
    /// 修改：
    /// </summary>
    class UserRepository:BaseRepository<User>,InterfaceUserRepository
    {

    }
}
