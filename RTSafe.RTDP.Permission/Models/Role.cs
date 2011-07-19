using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSafe.RTDP.Permission.Models
{
    public class Role
    {
        List<Operation> _operations = new List<Operation>();

        public List<Operation> Operations
        { get { return _operations; } }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
