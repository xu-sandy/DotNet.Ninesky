using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.Models;
using System.Security.Claims;

namespace Ninesky.IBLL
{
    /// <summary>
    /// 用户相关接口
    /// 创建：15.04.22 22：27
    /// 创建：徐大帅
    /// 修改：
    /// </summary>
    public interface InterfaceUserService:InterfaceBaseService<User>
    {
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>bool 结果</returns>
        bool Exist(string userName);
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <returns></returns>
        User Find(int userID);
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        User Find(string userName);
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="order">排序：0-ID升序（默认），1ID降序，2注册时间升序，3注册时间降序，4登录时间升序，5登录时间降序</param>
        /// <returns></returns>
        IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order);

        /// <summary>
        /// ASP.NET Identity身份认证系统的登录时要用到
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        ClaimsIdentity CreateIdentity(User user, string authenticationType);
    }
}
