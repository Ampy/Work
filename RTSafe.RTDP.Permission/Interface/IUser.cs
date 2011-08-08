using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RTSafe.RTDP.Permission.Interface
{
    public interface IUser : IDisposable
    {
        void Add(Models.User user);
        void Delete(Models.User user);
        void Edit(Models.User user);
        Models.User Find(Guid userId);
        List<Models.User> GetUsers();
        IQueryable<Models.User> Where(Expression<Func<Models.User, bool>> pre);
    }
}