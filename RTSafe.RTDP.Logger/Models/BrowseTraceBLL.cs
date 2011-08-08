using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTSafe.RTDP.Logger.Repository;

namespace RTSafe.RTDP.Logger.Models
{
    public partial class BrowseTrace
    {
        public void Save()
        {
            using (LoggerDbContext db = new LoggerDbContext())
            {
                db.BrowseTraces.Add(this);
                db.SaveChanges();
            }
        }
    }
}
