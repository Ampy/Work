using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSafe.RTDP.Permission.Interface
{
    public partial interface IModule
    {
        void Registe(Models.Module module);
        void Unregiste(Guid moduleId);
        void Edit(Models.Module module);
        List<Models.Module> GetAllModules(bool isLazy);
        Models.Module Find(Guid id);
    }
}
