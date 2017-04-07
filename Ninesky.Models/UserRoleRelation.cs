using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户角色关系
    /// 创建：15.04.23
    /// 创建：徐大帅
    /// 修改：
    /// </summary>
    public class UserRoleRelation
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public int RelationID { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [Required()]
        public int UserID { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        [Required()]
        public int RoelID  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual User Users { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
