using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RTSafe.RTDP.Permission.Models
{
    
    public class Module
    {
        [ForeignKey("ModuleId")]
        public List<Operation> Operations { get; set; }

        #region 数据字段
        private System.Guid _ModuleId;
        [Display(Name="标识")]
        [Key]
        public System.Guid ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }

        private System.String _Name;
        [Display(Name = "名称")]
        public System.String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private System.String _Description;
        [Display(Name = "描述")]
        public System.String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        #endregion

    }
}
