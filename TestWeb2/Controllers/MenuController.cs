using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTSafe.RTDP.Permission.Models;
using RTSafe.RTDP.Permission.DAL;

namespace TestWeb2.Controllers
{ 
    public class MenuController : Controller
    {
        private RTDPDbContext db = new RTDPDbContext();

        //
        // GET: /Menu/

        public ViewResult Index()
        {
            var menus = db.Menus.Include(m => m.ParentMenu);
            return View(menus.ToList());
        }

        //
        // GET: /Menu/Details/5

        public ViewResult Details(Guid id)
        {
            Menu menu = db.Menus.Find(id);
            return View(menu);
        }

        //
        // GET: /Menu/Create

        public ActionResult Create()
        {
            ViewBag.ParentMenuId = new SelectList(db.Menus, "MenuId", "Name");
            return View();
        } 

        //
        // POST: /Menu/Create

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.MenuId = Guid.NewGuid();
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ParentMenuId = new SelectList(db.Menus, "MenuId", "Name", menu.ParentMenuId);
            return View(menu);
        }
        
        //
        // GET: /Menu/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Menu menu = db.Menus.Find(id);
            ViewBag.ParentMenuId = new SelectList(db.Menus, "MenuId", "Name", menu.ParentMenuId);
            return View(menu);
        }

        //
        // POST: /Menu/Edit/5

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentMenuId = new SelectList(db.Menus, "MenuId", "Name", menu.ParentMenuId);
            return View(menu);
        }

        //
        // GET: /Menu/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Menu menu = db.Menus.Find(id);
            return View(menu);
        }

        //
        // POST: /Menu/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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