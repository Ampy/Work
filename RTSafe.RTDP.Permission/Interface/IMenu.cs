using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RTSafe.RTDP.Permission.Interface
{
    public partial interface IMenu
    {
        void Add(Models.Menu menu);
        void Delete(Models.Menu menu);
        void Edit(Models.Menu menu);
        Models.Menu Find(Guid menuId);
        List<Models.Menu> GetMenus();
        IQueryable<Models.Menu> Where(Expression<Func<Models.Menu, bool>> pre);
    }
}
