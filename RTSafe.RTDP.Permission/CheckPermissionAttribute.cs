using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Configuration;

namespace RTSafe.RTDP.Permission
{
    public class CheckPermissionAttribute : ActionFilterAttribute, IActionFilter
    {
        string _PermissionId;
        public CheckPermissionAttribute(string PermissionId)
        {
            _PermissionId = PermissionId;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RTDP.MVC.RtUser rtUser= filterContext.HttpContext.User as RTDP.MVC.RtUser;
            if (rtUser.IsInPermission(_PermissionId))
            {
                base.OnActionExecuting(filterContext);
            }

            else
            {
                string denyurl = ConfigurationManager.AppSettings["AccessDeny"];
                filterContext.Result = new AccessDenyResult();
            }

        }

    }


}
