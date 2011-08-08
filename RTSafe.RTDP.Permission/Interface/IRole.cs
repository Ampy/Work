using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RTSafe.RTDP.Permission.Interface
{
    public interface IRole:IDisposable
    {
        void Add(Models.Role role);
        void Delete(Models.Role role);
        void Edit(Models.Role role);
        Models.Role Find(Guid roleId,bool loadOperations);
        List<Models.Role> GetRoles(bool loadOperations);
        IQueryable<Models.Role> Where(Expression<Func<Models.Role, bool>> pre, bool loadOperations);
    }
}
