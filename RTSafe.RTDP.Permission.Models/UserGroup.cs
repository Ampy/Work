using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSafe.RTDP.Permission.Models
{
    public class UserGroup
    {
        public List<Role> Roles { get; set; }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
