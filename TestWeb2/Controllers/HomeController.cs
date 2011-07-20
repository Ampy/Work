using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTSafe.RTDP.MVC;
using RTSafe.RTDP.Permission;

namespace TestWeb2.Controllers
{
    public class HomeController : RtController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [CheckPermission("{6396B227-ACD8-475A-9865-F38FD6A19559}")]
        public ActionResult About()
        {
            //ViewBag.CanDelete = Model.Validate("aaa");
            //ViewBag.CanAdd = false;



            return View();
        }

        public ActionResult AccessDeny()
        {
            return View();
        }
    }
}
