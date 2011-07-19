﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSafe.RTDP.Permission.Models
{
    public class User
    {
         List<Role> _roles = new List<Role>();
        public User()
        {
        }
        public List<Role> Roles
        {
            get{return _roles;}
        }

        public UserGroup Group
        {
            get;
            set;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        
    }
}
