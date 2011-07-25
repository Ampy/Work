using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTSafe.RTDP.Permission.Models;

namespace TestWeb2.Controllers
{
    public class MPController : Controller
    {
        //
        // GET: /MP/
        public ActionResult Index()
        {
            MP mp = new MP();
            mp.ID = 111;
            mp.Name = "Ampy";
            return View(mp);
        }
        [HttpPost]
        public ActionResult Index(MP mp)
        {
            return View(mp);
        }

    }
}
