using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RTSafe.RTDP.Permission.Models
{
    public class Module
    {
        public List<Operation> Operations { get; set; }

        #region 数据字段
        private System.Guid _ID;
        public System.Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private System.String _Name;
        public System.String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private System.String _Description;
        public System.String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        #endregion

    }
}
