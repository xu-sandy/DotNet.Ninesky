using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户模型
    /// 创建： 2015.04.20  23:04:00
    /// 创建： 徐大帅
    /// 修改： 
    /// </summary>
    public class UserConfig
    {
        [Key]
        public int ConfigID { get; set; }
        /// <summary>
        /// 启用注册
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "启用注册")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 禁止使用的用户名
        /// 用户名之间用|分隔
        /// </summary>
        [Display(Name = "禁止使用的用户名")]
        public string ProhibitUserName { get; set; }
        /// <summary>
        /// 启用管理员验证
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "启用管理员验证")]
        public bool EnableAdminVerify { get; set; }
        /// <summary>
        /// 启用邮箱验证
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "启用邮箱验证")]
        public bool EnableEmailVerify { get; set; }
        /// <summary>
        /// 默认用户组id
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "默认用户组id")]
        public int DefaultGroupId { get; set; }
    }
}
