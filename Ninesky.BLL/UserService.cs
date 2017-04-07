using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.Models;
using Ninesky.DAL;
using Ninesky.IBLL;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Ninesky.BLL
{
    /// <summary>
    /// 用户服务基类
    /// 创建：15.04.22 22：50
    /// 创建：徐大帅
    /// 修改：
    /// </summary>
    public class UserService : BaseService<User>, InterfaceUserService
    {
        public UserService() : base(RepositoryFactory.UserRepository) { }
        /// <summary>
        /// 根据用户名判断用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Exist(string userName)
        {
            return CurrentRepository.Exist(u => u.UserName == userName);
        }
        /// <summary>
        /// 根据id查找user实体
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public User Find(int userID)
        {
            return CurrentRepository.Find(u => u.UserID == userID);
        }
        /// <summary>
        /// 根据用户名查找实体
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User Find(string userName)
        {
            return CurrentRepository.Find(u => u.UserName == userName);
        }
        public IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order)
        {
            bool _isAsc = true;
            string _orderName = string.Empty;
            switch (order)
            {
                case 0:
                    _isAsc = true;
                    _orderName = "UserID";
                    break;
                case 1:
                    _isAsc = false;
                    _orderName = "UserID";
                    break;
                case 2:
                    _isAsc = true;
                    _orderName = "RegistrationTime";
                    break;
                case 3:
                    _isAsc = false;
                    _orderName = "RegistrationTime";
                    break;
                case 4:
                    _isAsc = true;
                    _orderName = "LoginTime";
                    break;
                case 5: _isAsc = false;
                    _orderName = "LoginTime";
                    break;
                default:
                    _isAsc = false;
                    _orderName = "UserID";
                    break;
            }
            return CurrentRepository.FindPageList(pageIndex, pageSize,out totalRecord, u => true, _orderName, _isAsc);
        }
        /// <summary>
        /// ASP.NET Identity身份认证系统的登录时要用到
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public ClaimsIdentity CreateIdentity(User user, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
            _identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            _identity.AddClaim(new Claim("DisplayName", user.DisplayName));
            return _identity;
        }

    }
}
