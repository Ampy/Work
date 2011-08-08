using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTSafe.RTDP.Permission.Models;
using RTSafe.RTDP.Permission.Interface;
using RTSafe.RTDP.Permission.DAL;


namespace RTSafe.RTDP.Permission.BLL
{
    public class ModuleManager : IModule, IDisposable
    {
        RTDPDbContext db = new RTDPDbContext();
        public void Registe(Models.Module module)
        {
            db.Modules.Add(module);
            db.SaveChanges();
        }

        public void Unregiste(Guid moduleId)
        {
            Module m = db.Modules.Find(moduleId);
            db.Modules.Remove(m);
            db.SaveChanges();
        }

        public void Edit(Models.Module module)
        {
            db.Entry(module).State = EntityState.Modified;
            db.SaveChanges();

        }



        public List<Models.Module> GetAllModules(bool isLazy)
        {
            db.Configuration.LazyLoadingEnabled = false;

            if (isLazy)
                return db.Modules.ToList();
            else
                return db.Modules.Include(o => o.Operations).ToList();
        }

        public Models.Module Find(Guid id)
        {
            return db.Modules.Find(id);
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}
