﻿using System;
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
        //private RTDPDbContext db = new RTDPDbContext();

        private RTSafe.RTDP.Permission.BLL.OperationManager om = new RTSafe.RTDP.Permission.BLL.OperationManager();
        //
        // GET: /Operation/
        public ViewResult Index(Guid id)
        {
            ViewBag.ModuleId = id;
            return View(om.Where(c => c.ModuleId == id).ToList());            
            //return View(db.Operations.Where(c => c.ModuleId == id).ToList());
        }

        //
        // GET: /Operation/Details/5

        public ViewResult Details(Guid id)
        {
            Operation operation = om.Find(id);//db.Operations.Find(id);
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
                operation.OperationId = Guid.NewGuid();
                om.Add(operation);
                //db.Operations.Add(operation);
                //db.SaveChanges();
                return RedirectToAction("Index/" + operation.ModuleId.ToString());  
            }

            return View(operation);
        }
        
        //
        // GET: /Operation/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Operation operation = om.Find(id);//db.Operations.Find(id);
            return View(operation);
        }

        //
        // POST: /Operation/Edit/5

        [HttpPost]
        public ActionResult Edit(Operation operation)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(operation).State = EntityState.Modified;
                //db.SaveChanges();
                om.Edit(operation);
                return RedirectToAction("Index/" + operation.ModuleId.ToString());
            }
            return View(operation);
        }

        //
        // GET: /Operation/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Operation operation = om.Find(id);//db.Operations.Find(id);
            return View(operation);
        }

        //
        // POST: /Operation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Operation operation = om.Find(id); //db.Operations.Find(id);
            //db.Operations.Remove(operation);
            //db.SaveChanges();
            om.Delete(id);
            return RedirectToAction("Index/" + operation.ModuleId.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            om.Dispose();
            base.Dispose(disposing);
        }

  
    }
}