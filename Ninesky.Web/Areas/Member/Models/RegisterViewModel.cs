using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Web.Areas.Member.Models
{
    /// <summary>
    /// 用户注册试图模型
    /// 创建：15.03.25 00：10
    /// 创建：徐大帅
    /// 修改：
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage=("必填"))]
        [StringLength(20,MinimumLength=4,ErrorMessage="{1}到{0}个字符")]
        [Display(Name="用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        [Required(ErrorMessage=("必填"))]
        [StringLength(20,MinimumLength=2,ErrorMessage="{1}到{0}字符")]
        [Display(Name="显示名")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage="必填")]
        [StringLength(20,MinimumLength=6,ErrorMessage="{1}到{0}个字符")]
        [Display(Name="密码")]
        [DataType(DataType.Password)]
        public string Password  { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage="必填")]
        [Compare("Password",ErrorMessage="两次输入的密码不一致")]
        [DataType(DataType.Password)]
        [Display(Name="确认密码")]
        public string ConfirmPassword  { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage="必填")]
        [Display(Name="邮箱")]
        [DataType(DataType.EmailAddress,ErrorMessage="邮箱格式不正确")]
        public string  Email  { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage="必填")]
        [StringLength(6,MinimumLength=6,ErrorMessage="验证码不正确")]
        [Display(Name="验证码")]
        public string VerificationCode  { get; set; }
    }
}