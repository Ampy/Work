﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RTSafe.RTDP.Permission.Models
{
    public partial class Operation
    {
        [NotMapped]
        public bool hasOperation { get; set; }

        [NotMapped]
        public Guid CurrentRoleId { get; set; }


    }
}
