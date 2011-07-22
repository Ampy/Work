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
        //List<Operation> _operations = new List<Operation>();
  
        //public List<Operation> Operations
        //{ 
        //    get { return _operations; } 
        //}


        [Key]
        [Display(Name = "标识")]
        public Guid RoleId { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
