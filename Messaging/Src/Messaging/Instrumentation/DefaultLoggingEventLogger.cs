﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Logging Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using RTSafe.RTDP.Messaging.Properties;

namespace RTSafe.RTDP.Messaging.Instrumentation
{

    /// <summary>
    /// The instrumentation gateway when no instances of the objects from the block are involved.
    /// </summary>
    [EventLogDefinition("Application", "Enterprise Library Logging")]
    public class DefaultLoggingEventLogger : InstrumentationListener
    {
        private readonly IEventLogEntryFormatter eventLogEntryFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLoggingEventLogger"/> class, specifying whether 
        /// logging to the event log is allowed.
        /// </summary>
        /// <param name="eventLoggingEnabled"><code>true</code> if writing to the event log is allowed, <code>false</code> otherwise.</param>
        public DefaultLoggingEventLogger(bool eventLoggingEnabled)
            : base((string)null, false, eventLoggingEnabled, null)
        {
            this.eventLogEntryFormatter = new EventLogEntryFormatter(Resources.BlockName);
        }

        /// <summary>
        /// Logs the occurrence of a configuration error for the Enterprise Library Logging Application Block through the 
        /// available instrumentation mechanisms.
        /// </summary>
        /// <param name="exception">The exception raised for the configuration error.</param>
        public void LogConfigurationError(Exception exception)
        {
            if(exception == null) throw new ArgumentNullException("exception");

            if (EventLoggingEnabled)
            {
                string entryText = eventLogEntryFormatter.GetEntryText(Resources.ConfigurationFailureLogging, exception);
                EventLog.WriteEntry(GetEventSourceName(), entryText, EventLogEntryType.Error);
            }
        }
    }
}
