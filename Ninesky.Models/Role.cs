using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户角色
    /// 创建：15.04.23
    /// 创建:徐大帅
    /// 修改：
    /// </summary>
    public class Role
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage="必填")]
        [StringLength(20,MinimumLength=2,ErrorMessage="{1}到{0}个字符之间")]
        [Display(Name="名称")]
        public string Name { get; set; }
        /// <summary>
        /// 用户组类型
        /// 0普通（普通注册用户），1特权（像VIP之类的类型），3管理（管理权限的类型）
        /// </summary>
        [Required(ErrorMessage="必填")]
        [Display(Name="用户组名称")]
        public int Type { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Required(ErrorMessage="必填")]
        [StringLength(20,ErrorMessage="少于{0}个字符")]
        public string Description  { get; set; }
        /// <summary>
        /// 获取角色类型名称
        /// </summary>
        /// <returns></returns>
        public string TypeToSting() 
        {
            switch (Type)
            {
                case 0:
                    return "普通";
                case 1:
                    return "特权";
                case 2:
                    return "管理";
                default:
                    return "未知";
            }
        }
    }
}
