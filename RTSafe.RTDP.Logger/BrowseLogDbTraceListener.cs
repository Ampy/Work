using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Database.Configuration;
using RTSafe.RTDP.Logger.Models;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace RTSafe.RTDP.Logger
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class BrowseLogDbTraceListener : CustomTraceListener
    {
        public void Write(string sessionId, Guid userId, string message)
        {
            BrowseTrace bt = new BrowseTrace();
            bt.SessionID = sessionId;
            bt.UserID = userId;
        }

        public override void Write(string message)
        {

            throw new NotImplementedException();
        }

        public override void WriteLine(string message)
        {
            throw new NotImplementedException();
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {

                        LogEntry logEntry = data as LogEntry;
                        BrowseTrace bt = new BrowseTrace();
                        bt.SessionID = logEntry.ExtendedProperties["SessionId"].ToString();
                        bt.UserID = Guid.Parse(logEntry.ExtendedProperties["UserId"].ToString());
                        bt.EnterTime = DateTime.Now;
                        bt.PreviousUrl = logEntry.ExtendedProperties["PreviousUrl"].ToString();
                        bt.NowUrl = logEntry.ExtendedProperties["NowUrl"].ToString();
                        bt.Save();

                }
                else if (data is string)
                {
                    Write(data as string);
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        /// <summary>
        /// Declare the supported attributes for <see cref="FormattedDatabaseTraceListener"/>
        /// </summary>
        protected override string[] GetSupportedAttributes()
        {
            return new string[4] { "formatter", "writeLogStoredProcName", "addCategoryStoredProcName", "databaseInstanceName" };
        }


    }
}
