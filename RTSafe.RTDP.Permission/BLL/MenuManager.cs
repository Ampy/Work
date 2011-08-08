using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTSafe.RTDP.Permission.Interface;
using RTSafe.RTDP.Permission.DAL;
using RTSafe.RTDP.Permission.Models;

namespace RTSafe.RTDP.Permission.BLL
{
    public class MenuManager : IMenu
    {
        RTDPDbContext db = new RTDPDbContext();

        public void Add(Models.Menu menu)
        {
            db.Menus.Add(menu);
            db.SaveChanges();
        }

        public void Delete(Models.Menu menu)
        {
            db.Menus.Remove(menu);
            db.SaveChanges();
        }

        public void Edit(Models.Menu menu)
        {
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Models.Menu Find(Guid menuId)
        {
            return db.Menus.Find(menuId);
        }

        public List<Models.Menu> GetMenus()
        {
            return db.Menus.ToList();
        }

        public IQueryable<Models.Menu> Where(System.Linq.Expressions.Expression<Func<Models.Menu, bool>> pre)
        {

            return db.Menus.Where(pre);

        }

        public void Dispose()
        {
            db.Dispose();
        }


        public Models.Menu Find(Guid MenuId, bool loadOperations = false)
        {
                return db.Menus.Find(MenuId);
        }

        public void RemoveOperations(Menu menu)
        {
            db.Menus.Remove(menu);
            //Menu.Operations.Remove(
        }

    }
}
