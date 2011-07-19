using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics;

namespace RTSafe.RTDP.MVC
{
    public class RtController:Controller
    {
        public RtController():base()
        {            
            //读取数据库或缓存或cookie获得User的Roles,Permissions。
            if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                RTDP.MVC.RtUser rtUser = new RtUser(System.Web.HttpContext.Current.User.Identity.Name, "");
                System.Web.HttpContext.Current.User = rtUser;
            }
        }

    }
}
