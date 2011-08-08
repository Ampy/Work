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
        //private RTDPDbContext db = new RTDPDbContext();
        private RTSafe.RTDP.Permission.BLL.RoleManager rm = new RTSafe.RTDP.Permission.BLL.RoleManager();
        //
        // GET: /Role/

        public ViewResult Index()
        {
            return View(rm.GetRoles(true));
        }

        //
        // GET: /Role/Details/5

        public ViewResult Details(Guid id)
        {
            Role role = rm.Find(id, true);// db.Roles.Find(id);
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
                //db.Roles.Add(role);
                //db.SaveChanges();
                rm.Delete(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(Guid id)
        {
            Role role = rm.Find(id); //db.Roles.Find(id);
            return View(role);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(role).State = EntityState.Modified;
                //db.SaveChanges();
                rm.Edit(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(Guid id)
        {
            Role role = rm.Find(id); //db.Roles.Find(id);
            return View(role);
        }

        [HttpPost]
        public ActionResult Delete(Role role)
        {
            //Role role = db.Roles.Find(id);
            //db.Roles.Remove(role);
            //db.SaveChanges();
            rm.Delete(role);
            return RedirectToAction("Index");
        }

        //
        // POST: /Role/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Role role = db.Roles.Find(id);
        //    db.Roles.Remove(role);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            rm.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult RoleOperation(string id)
        {
            Guid roleId = Guid.Parse(id);
            Role role = rm.Find(roleId);

            RTSafe.RTDP.Permission.BLL.ModuleManager mm = new RTSafe.RTDP.Permission.BLL.ModuleManager();

            List<Module> modules = mm.GetAllModules(false);// rm..Include(c=>c.Operations).ToList();

            foreach (var m in modules)
            {
                foreach (var o in m.Operations)
                {
                    o.hasOperation = role.Operations.SingleOrDefault(c => c.OperationId == o.OperationId) == null ? false : true;
                }
            }

            return View(modules);
        }

        [HttpPost]
        public ActionResult RoleOperation(string id, FormCollection collection)
        {
            Guid roleId = Guid.Parse(id);

            Role role = rm.Find(roleId, true);//.SingleOrDefault(c=>c.RoleId==roleId);


            List<Operation> delops = role.Operations.ToList();

            foreach (var m in delops)
            {
                role.Operations.Remove(m);
            }
            //rm.SaveChanges();
            rm.Edit(role);
            if (collection["operation"] != null)
            {
                //RTSafe.RTDP.Permission.BLL.OperationManager om = new RTSafe.RTDP.Permission.BLL.OperationManager();
                foreach (string opid in collection["operation"].Split(','))
                {
                    //Operation op = om.Find(Guid.Parse(opid));
                    //role.Operations.Add(op);
                    rm.AddOperation(role, opid);
                }
                //db.SaveChanges();
                rm.Edit(role);
                //om.Dispose();
            }
           
            return RedirectToAction("RoleOperation/" + @roleId.ToString());
        }

        public ActionResult RoleMenu(string id)
        {
            Guid roleId = Guid.Parse(id);
            Role role = rm.Find(roleId);

            RTSafe.RTDP.Permission.BLL.MenuManager mm = new RTSafe.RTDP.Permission.BLL.MenuManager();
            
            //List<Module> modules = mm.GetAllModules(false);// rm..Include(c=>c.Operations).ToList();
            List<Menu> menus = mm.GetMenus();

                foreach (var o in menus)
                {
                    o.hasMenu = role.Menus.SingleOrDefault(c => c.MenuId == o.MenuId) == null ? false : true;
                }

                return View(menus);
        }

        [HttpPost]
        public ActionResult RoleMenu(string id, FormCollection collection)
        {
            Guid roleId = Guid.Parse(id);

            Role role = rm.Find(roleId, true);//.SingleOrDefault(c=>c.RoleId==roleId);


            List<Menu> delops = role.Menus.ToList();

            foreach (var m in delops)
            {
                role.Menus.Remove(m);
            }
            //rm.SaveChanges();
            rm.Edit(role);
            if (collection["menu"] != null)
            {
                //RTSafe.RTDP.Permission.BLL.OperationManager om = new RTSafe.RTDP.Permission.BLL.OperationManager();
                foreach (string opid in collection["menu"].Split(','))
                {
                    //Operation op = om.Find(Guid.Parse(opid));
                    //role.Operations.Add(op);
                    rm.AddMenu(role, opid);
                }
                //db.SaveChanges();
                rm.Edit(role);
                //om.Dispose();
            }

            return RedirectToAction("RoleMenu/" + @roleId.ToString());
        }
    }
}