using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Web.Areas.Member.Models
{
    /// <summary>
    /// 更改密码viewmodel
    /// 创建：2015.04.27 23:31
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Required(ErrorMessage="必填")]
        [StringLength(20,MinimumLength=6,ErrorMessage="{2}到{1}个字符")]
        [DataType(DataType.Password)]
        [Display(Name="原密码")]
        public string OriginalPassword  { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage="必填")]
        [StringLength(20,MinimumLength=6,ErrorMessage="{2}到{1}个字符")]
        [Display(Name="新密码")]
        [DataType(DataType.Password)]
        public string Password  { get; set; }
        /// <summary>
        /// 确认新密码
        /// </summary>
        [Required(ErrorMessage="必填")]
        [Display(Name="确认密码")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage="两次输入的密码不一致")]
        public string ConfirmPassword  { get; set; }
    }
}