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
    public class OperationController : Controller
    {
        private RTDPDbContext db = new RTDPDbContext();

        //
        // GET: /Operation/

        public ViewResult Index(Guid id)
        {
            ViewBag.ModuleId = id;
            return View(db.Operations.Where(c => c.ModuleId == id).ToList());
        }

        //
        // GET: /Operation/Details/5

        public ViewResult Details(Guid id)
        {
            Operation operation = db.Operations.Find(id);
            return View(operation);
        }

        //
        // GET: /Operation/Create

        public ActionResult Create(Guid moduleId)
        {
 
            return View();
        } 

        //
        // POST: /Operation/Create

        [HttpPost]
        public ActionResult Create(Operation operation)
        {
            if (ModelState.IsValid)
            {
                operation.ID = Guid.NewGuid();
                db.Operations.Add(operation);
                db.SaveChanges();
                return RedirectToAction("Index/" + operation.ModuleId.ToString());  
            }

            return View(operation);
        }
        
        //
        // GET: /Operation/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Operation operation = db.Operations.Find(id);
            return View(operation);
        }

        //
        // POST: /Operation/Edit/5

        [HttpPost]
        public ActionResult Edit(Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + operation.ModuleId.ToString());
            }
            return View(operation);
        }

        //
        // GET: /Operation/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Operation operation = db.Operations.Find(id);
            return View(operation);
        }

        //
        // POST: /Operation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Operation operation = db.Operations.Find(id);
            db.Operations.Remove(operation);
            db.SaveChanges();
            return RedirectToAction("Index/" + operation.ModuleId.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}