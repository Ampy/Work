using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RTSafe.RTDP.Permission
{
    public class AccessDenyResult:ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Redirect("~/Home/AccessDeny");
            //throw new NotImplementedException();
        }
    }
}
