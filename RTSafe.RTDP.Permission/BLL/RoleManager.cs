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
    public class RoleManager:IRole
    {
        RTDPDbContext db = new RTDPDbContext();

        public void Add(Models.Role role)
        {
            db.Roles.Add(role);
            db.SaveChanges();
        }

        public void Delete(Models.Role role)
        {
            db.Roles.Remove(role);
            db.SaveChanges();
        }

        public void Edit(Models.Role role)
        {
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Models.Role Find(Guid roleId)
        {
            return db.Roles.Find(roleId);
        }

        public List<Models.Role> GetRoles(bool loadOperations)
        {
            if (loadOperations)
            {
                return db.Roles.Include(c => c.Operations).ToList();
            }
            else
            {
                return db.Roles.ToList();
            }
        }

        public IQueryable<Models.Role> Where(System.Linq.Expressions.Expression<Func<Models.Role, bool>> pre, bool loadOperations)
        {
            if (loadOperations)
            {
                return db.Roles.Include(c => c.Operations).Where(pre);
            }
            else
            {
                return db.Roles.Where(pre);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }


        public Models.Role Find(Guid roleId, bool loadOperations=false)
        {
            //db.Roles.Select(c => new { A = c.RoleId,B=c.Name });
            if (loadOperations)
            {
                return db.Roles.Include(c => c.Operations).SingleOrDefault(c => c.RoleId == roleId);
            }
            else
            {
                Role r = new Role();

                return db.Roles.Find(roleId);
            }

        }

        public void RemoveOperations(Role role)
        {
            //role.Operations.Remove(
        }

        public void AddOperation(Role role, string operationId)
        {
            Operation op = db.Operations.Find(Guid.Parse(operationId));
            role.Operations.Add(op);
            
        }

        public void AddMenu(Role role, string menuId)
        {
            Menu op = db.Menus.Find(Guid.Parse(menuId));
            role.Menus.Add(op);

        }
    }
}
