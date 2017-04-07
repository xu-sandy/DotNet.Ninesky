using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户模型
    /// 创建： 2015.04.20  22:10:00
    /// 创建： 徐大帅
    /// 修改： 
    /// </summary>
    public class User
    {

        [Key]
        public int UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{1}到{0}个字符")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户组ID
        /// </summary>
       // [Required(ErrorMessage = "必填")]
       // [Display(Name = "用户组ID")]
        //public int GroupID { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字符")]
        [Display(Name = "显示名")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// 用户状态
        /// 0=正常，1=锁定，2=未通过邮件验证，3=未通过管理员验证
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegistrationTime { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 登录ip
        /// </summary>
        public string LoginIP { get; set; }
        /// <summary>
        /// 与用户组的关联
        /// </summary>
       // public virtual UserGroup Group { get; set; }
        public virtual IQueryable<UserRoleRelation> UserRoleRelations { get; set; }
    }
}
