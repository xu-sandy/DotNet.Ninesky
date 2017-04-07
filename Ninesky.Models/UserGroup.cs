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
    /// 创建： 2015.04.20  22:54:00
    /// 创建： 徐大帅
    /// 修改： 
    /// </summary>
    public class UserGroup
    {
        [Key]
        public int GroupID { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字符")]
        [Display(Name = "名称")]
        public string Name { get; set; }
        /// <summary>
        /// 组类型
        /// 0普通类型（普通注册用户） 1特权类型（比如vip） 2 管理类型（管理权限类型）
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "用户组类型")]
        public int GroupType { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringLength(50, ErrorMessage = "少于{0}个字")]
        [Display(Name = "说明")]
        public string Description { get; set; }
    }
}
