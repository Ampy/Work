using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RTSafe.RTDP.Permission.Models
{
    public partial class RoleOperation
    {

        [Key]
        public Guid RoleId { get; set; }
        [Key]
        public Guid OperationId { get; set; }
    }
}
