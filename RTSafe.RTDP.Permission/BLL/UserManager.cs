using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTSafe.RTDP.Permission.Interface;
using RTSafe.RTDP.Permission.DAL;
using RTSafe.RTDP.Permission.Models;
using System.Data;

namespace RTSafe.RTDP.Permission.BLL
{
    public class UserManager:IUser
    {
        RTDPDbContext db = new RTDPDbContext();

        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Delete(User user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public void Edit(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public User Find(Guid userId)
        {
            return db.Users.Find(userId);
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public IQueryable<User> Where(System.Linq.Expressions.Expression<Func<User, bool>> pre)
        {
            return db.Users.Where(pre);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
