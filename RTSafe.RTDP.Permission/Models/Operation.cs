﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSafe.RTDP.Permission.Models
{
    public class Operation
    {
        public Guid ModuleId { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}