using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RTSafe.RTDP.Permission.Models
{
    [Table("Menu")]
    public partial class Menu
    {
        //[Key]
        //public Guid MenuId { get; set; }

        //public Guid ParentMenuId { get; set; }

        //public string Name { get; set; }

        //public string Description { get; set; }

        //public string EntryUrl { get; set; }

        ////public virtual Guid Menu_MenuId { get; set; }
        //public virtual Menu ParentMenu { get; set; }       
        //public virtual ICollection<Menu> ChildrenMenus{get;set;}

        
        public System.Guid MenuId { get; set; }
        public Nullable<System.Guid> ParentMenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EntryUrl { get; set; }
        public int DisplayOrder { get; set; }
        public virtual ICollection<Menu> ChildrenMenus { get; set; }
        public virtual Menu ParentMenu { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
