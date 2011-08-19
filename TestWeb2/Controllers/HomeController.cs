using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTSafe.RTDP.MVC;
using RTSafe.RTDP.Permission;
using RTSafe.RTDP.Messaging;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace TestWeb2.Controllers
{
    public class HomeController : RtController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            
            return View();
        }

        [HttpPost]
        public ViewResult Index(FormCollection forms)
        {
            //Messager.Write("OK");
            //Logger.Write("OK");
            MsgEntry ms = new MsgEntry();
            ms.Message = "测试！";
            ms.CreateTime = DateTime.Now;
            ms.Receiver = "JLX";
            ms.Sender = "MP";
            ms.Severity = System.Diagnostics.TraceEventType.Information;
            ms.Priority = 0;
            ms.Categories.Add("SMS");
            ms.Categories.Add("EMAIL");
            Messager.Write(ms);
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
