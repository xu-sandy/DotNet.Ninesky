using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninesky.Common;
using System.Drawing;
using Ninesky.Web.Areas.Member.Models;
using Ninesky.IBLL;
using Ninesky.BLL;
using Ninesky.Models;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace Ninesky.Web.Areas.Member.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //InterfaceUserService userService = new UserService();
        private InterfaceUserService userService;
        public UserController() { userService = new UserService(); }
        //
        // GET: /Member/User/
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult VerificationCode()
        {
            //获取随机生成的验证码字符
            string verificationCode = Security.CreateVerificationText(6);
            //生成指定宽高的图片
            Bitmap _bitmap = Security.CreateVerificationImage(verificationCode, 160, 30);
            //将图片写到流里面
            _bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //将验证码转大写存到tempdata里
            //tempdata数据只传递一次，传递到下一个action，action代码执行完后tempdata就销毁，存session的就会持续保存
            TempData["VerificationCode"] = verificationCode.ToUpper();
            return null;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel register)
        {
            //验证验证码是否正确
            if (TempData["VerificationCode"] == null || TempData["VerificationCode"].ToString() != register.VerificationCode.ToUpper())
            {
                //将指定的错误添加到关联的model中
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View(register);
            }
            // 验证该model中是否包含错误
            if (ModelState.IsValid)
            {
                //调用bll接口方法判断用户是否存在
                if (userService.Exist(register.UserName))
                {
                    //如果用户名存在就网model状态里添加用户名已存在的错误
                    ModelState.AddModelError("UserName", "用户名已存在");
                }
                else
                {
                    User _user = new User()
                    {
                        UserName = register.UserName,
                        //todo:默认用户组代码
                        DisplayName = register.DisplayName,
                        Password = Security.sha256(register.Password),
                        //todo:邮箱验证与唯一的验证
                        Email = register.Email,
                        //用户状态
                        Status = 0,
                        RegistrationTime = DateTime.Now,
                        LoginTime = DateTime.Now
                    };
                    //调用接口方法实现数据插入
                    _user = userService.Add(_user);
                    //判断是否插入成功
                    if (_user.UserID > 0)
                    {
                        //return Content("注册成功！");
                        //直接跳转到登录页面
                        var _identity = userService.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignIn(_identity);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "注册失败！");
                    }
                }
            }
            return View(register);
        }
        #region 获取AuthenticationManager（认证管理器）
        public IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
        #endregion
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="returnUrl">返回url</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }
        /// <summary>
        /// 用户登录表单处理
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var _user = userService.Find(loginViewModel.UserName);
                if (_user == null)
                {
                    ModelState.AddModelError("UserName", "用户名不存在");
                }
                else if (_user.Password == Security.sha256(loginViewModel.Password))
                {
                    //最后登录时间
                    _user.LoginTime = DateTime.Now;
                    //最后登录ip
                    _user.LoginIP = Request.UserHostAddress;
                    //更新数据
                    userService.Update(_user);
                    var _identity = userService.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = loginViewModel.RememberMe }, _identity);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "密码错误");
                }
            }
            return View();
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect(Url.Content("~/"));
        }
        /// <summary>
        /// 左边菜单导航
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return PartialView();
        }
        /// <summary>
        /// 显示资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            return View(userService.Find(User.Identity.Name));
        }
        /// <summary>
        /// 部分更新用户数据
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Modify()
        {
            //找出当前对应数据
            var _user = userService.Find(User.Identity.Name);
            if (_user == null)
            {
                ModelState.AddModelError("UserName", "用户不存在");
            }
            else
            {
                //更新部分实体用tryupdate
                //这句话的意思是我只想更新用户提交的displayname 和 email
                if (TryUpdateModel(_user, new string[] { "DisplayName", "Email" }))
                {
                    if (ModelState.IsValid)
                    {
                        if (userService.Update(_user))
                        {
                            ModelState.AddModelError("", "修改成功");
                        }
                        else
                        {
                            ModelState.AddModelError("", "无需要修改的资料");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "更新模型数据失败");
                }
            }
            return View("Details", _user);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// 修改密码后台处理
        /// </summary>
        /// <param name="passwordViewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel passwordViewModel)
        {
            if (ModelState.IsValid)
            {
                var _user = userService.Find(User.Identity.Name);
                if (_user.Password == Security.sha256(passwordViewModel.OriginalPassword))
                {
                    _user.Password = Security.sha256(passwordViewModel.Password);
                    if (userService.Update(_user))
                    {
                        ModelState.AddModelError("", "修改密码成功");
                    }
                    else
                    {
                        ModelState.AddModelError("", "修改密码失败");
                    }
                }
                else
                {
                    ModelState.AddModelError("OriginalPassword", "密码错误");
                }
            }
            return View(passwordViewModel);
        }
    }
}