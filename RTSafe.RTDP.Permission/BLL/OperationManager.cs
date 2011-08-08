using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTSafe.RTDP.Permission.Interface;
using RTSafe.RTDP.Permission.DAL;
using System.Data;
using System.Linq.Expressions;

namespace RTSafe.RTDP.Permission.BLL
{
    public class OperationManager:IOperation
    {
        RTDPDbContext db = new RTDPDbContext();

        public void Add(Models.Operation operation)
        {

            db.Operations.Add(operation);
            db.SaveChanges();
        }

        public void Delete(Guid operationId)
        {
            Models.Operation operation = db.Operations.Find(operationId);
            db.Operations.Remove(operation);
            db.SaveChanges();
        }

        public void Edit(Models.Operation operation)
        {
            db.Entry(operation).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Models.Operation Find(Guid operationId)
        {
            return db.Operations.Find(operationId);
        }

        public void Dispose()
        {
            db.Dispose();
        }


        public List<Models.Operation> GetOperations(Guid moduleId)
        {
            var operations = db.Operations.Where((c => c.ModuleId == moduleId));
            return operations.ToList();
        }

        public IQueryable<Models.Operation> Where(Expression<Func<Models.Operation,bool>> pre)
        {
            return db.Operations.Where(pre);
        }
    }
}
