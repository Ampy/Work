using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTSafe.RTDP.Permission.Models;
using RTSafe.RTDP.Permission.DAL;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;

namespace TestWeb2.Controllers
{ 
    public class UserGroupController : Controller
    {
        private RTDPDbContext db = new RTDPDbContext();

        //
        // GET: /UserGroup/

        public ViewResult Index()
        {
            //try
            //{
                //int i = 0;
                //int t = 100 / i;
 //           }
            //catch (Exception ex)
            //{
            //    bool rethrow = false;
            //    var exManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();
            //    rethrow = exManager.HandleException(ex, "Policy");
            //    if (rethrow)
            //    {
            //        throw ex;
            //        //this.RedirectPermanent("~/error.aspx");
            //    }
            //}
            return View(db.UserGroups.ToList());
        }

        //
        // GET: /UserGroup/Details/5

        public ViewResult Details(Guid id)
        {

            UserGroup usergroup = db.UserGroups.Find(id);
            return View(usergroup);
        }

        //
        // GET: /UserGroup/Create

        public ActionResult Create()
        {
            int i = 0;
            int t = 100 / i;
            return View();
        } 

        //
        // POST: /UserGroup/Create

        [HttpPost]
        public ActionResult Create(UserGroup usergroup)
        {
            if (ModelState.IsValid)
            {
                usergroup.UserGroupID = Guid.NewGuid();
                db.UserGroups.Add(usergroup);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(usergroup);
        }
        
        //
        // GET: /UserGroup/Edit/5
 
        public ActionResult Edit(Guid id)
        {

            UserGroup usergroup = db.UserGroups.Find(id);
            return View(usergroup);
        }

        //
        // POST: /UserGroup/Edit/5

        [HttpPost]
        public ActionResult Edit(UserGroup usergroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usergroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usergroup);
        }

        //
        // GET: /UserGroup/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            UserGroup usergroup = db.UserGroups.Find(id);
            return View(usergroup);
        }

        //
        // POST: /UserGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            UserGroup usergroup = db.UserGroups.Find(id);
            db.UserGroups.Remove(usergroup);
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