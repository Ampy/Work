using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTSafe.RTDP.Permission.Models;
using TestWeb2.Models;

namespace TestWeb2.Controllers
{ 
    public class ModuleController : Controller
    {
        private RTDPDbContext db = new RTDPDbContext();

        //
        // GET: /Module/

        public ViewResult Index()
        {
            var m = db.Modules.Include(c=>c.Operations);
            return View(m);
        }

        //
        // GET: /Module/Details/5

        public ViewResult Details(Guid id)
        {
            Module module = db.Modules.Find(id);
            return View(module);
        }

        //
        // GET: /Module/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Module/Create

        [HttpPost]
        public ActionResult Create(Module module)
        {
            if (ModelState.IsValid)
            {
                module.ModuleId = Guid.NewGuid();
                db.Modules.Add(module);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(module);
        }
        
        //
        // GET: /Module/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Module module = db.Modules.Find(id);
            return View(module);
        }

        //
        // POST: /Module/Edit/5

        [HttpPost]
        public ActionResult Edit(Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(module);
        }

        //
        // GET: /Module/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Module module = db.Modules.Find(id);
            return View(module);
        }

        //
        // POST: /Module/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}