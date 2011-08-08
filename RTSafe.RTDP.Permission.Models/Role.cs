using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RTSafe.RTDP.Permission.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [Display(Name = "标识")]
        public Guid RoleId { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
