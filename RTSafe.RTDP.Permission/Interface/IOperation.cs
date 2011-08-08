using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSafe.RTDP.Permission.Interface
{
    public interface IOperation:IDisposable
    {
        void Add(Models.Operation operation);
        void Delete(Guid operationId);
        void Edit(Models.Operation operation);
        Models.Operation Find(Guid operationId);
        List<Models.Operation> GetOperations(Guid moduleId);

    }
}
