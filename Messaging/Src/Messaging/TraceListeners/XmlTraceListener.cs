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

using System.Diagnostics;
using System.IO;
using System.Xml.XPath;
using RTSafe.RTDP.Messaging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using RTSafe.RTDP.Messaging.Configuration;

namespace RTSafe.RTDP.Messaging.TraceListeners
{
    /// <summary>
    /// A <see cref="TraceListener"/> that writes an XML.
    /// </summary>
    [ConfigurationElementType(typeof(XmlTraceListenerData))]
    public class XmlTraceListener : XmlWriterTraceListener
    {
        /// <summary>
        /// Initializes a new instance of <see cref="XmlTraceListener"/>.
        /// </summary>
        public XmlTraceListener(string filename)
            : base(filename)
        { }

        /// <summary>
        /// Delivers the trace data as an XML message.
        /// </summary>
        /// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/>.</param>
        /// <param name="source">The name of the trace source that delivered the trace data.</param>
        /// <param name="eventType">The type of event.</param>
        /// <param name="id">The id of the event.</param>
        /// <param name="data">The data to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            object actualData = data;

            if (data is XmlLogEntry)
            {
                XmlLogEntry logEntryXml = data as XmlLogEntry;
                if (logEntryXml.Xml != null)
                {
                    actualData = logEntryXml.Xml;
                }
                else
                {
                    actualData = GetXml(logEntryXml);
                }
            }
            else if (data is MsgEntry)
            {
                actualData = GetXml(data as MsgEntry);
            }

            base.TraceData(eventCache, source, eventType, id, actualData);
        }

        internal virtual XPathNavigator GetXml(MsgEntry MsgEntry)
        {
            return new XPathDocument(new StringReader(new XmlMsgFormatter().Format(MsgEntry))).CreateNavigator();
        }
    }
}
