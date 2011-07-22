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
    public class RoleController : Controller
    {
        private RTDPDbContext db = new RTDPDbContext();

        //
        // GET: /Role/

        public ViewResult Index()
        {
            return View(db.Roles.Include(c=>c.Operations));
        }

        //
        // GET: /Role/Details/5

        public ViewResult Details(Guid id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                role.RoleId = Guid.NewGuid();
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(Guid id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(Guid id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // POST: /Role/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult RoleOperation(Guid roleId)
        {
            //Role role = db.Roles.Include(o => o.Operations).SingleOrDefault(c => c.ID == roleId);
            //List<Guid> ros = db.RoleOperations.Where(c => c.RoleId == roleId).Select(c => c.OperationId).ToList();
            //List<Guid> hasOperations = db.Operations.Where(c => ros.Contains(c.ID)).Select(m => m.ID).ToList();

            //List<Operation> operations = db.Operations.ToList();
            //foreach (var o in operations)
            //{
            //    if (hasOperations.Contains(o.ID))
            //        o.hasOperation = true;
            //    else
            //        o.hasOperation = false;
            //}
            Role role = db.Roles.Find(roleId);

            ICollection<Operation> ops = db.Operations.ToList();

            foreach (var o in ops)
            {
                o.hasOperation = role.Operations.SingleOrDefault(c => c.OperationId == o.OperationId) == null ? false : true;
            }

            return View(ops);
        }
    }
}