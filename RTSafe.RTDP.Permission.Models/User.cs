﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSafe.RTDP.Permission.Models
{
    public class User
    {
        //List<Role> _roles = new List<Role>();
        //List<Operation> Operations { get; set; }
        public User()
        {
        }
        //public List<Role> Roles
        //{
        //    get { return _roles; }
        //}

        //public UserGroup Group
        //{
        //    get;
        //    set;
        //}

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        //public virtual List<Operation> Operations { get; set; }
    }
}
